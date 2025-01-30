using Project.Ability;
using Project.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class BattleController : MonoBehaviour
	{
		[SerializeField] private BattleView _view = null;

		[Header("Players")]
		[SerializeField] private BasePlayer _leftPlayer = null;
		[SerializeField] private BasePlayer _rightPlayer = null;

		private bool _isLeftPlayerTurn = true;

		private List<IAbility> _leftPlayerAbilities = new();
		private List<IAbility> _rightPlayerAbilities = new();

		private void Awake()
		{
			if (ClientManager.Instance != null)
			{
				ClientManager.Instance.SetEntity += OnSetEntity;
				ClientManager.Instance.AbilitySelected += OnAbilitySelected;
				ClientManager.Instance.NextTurn += NextTurn;

				_view.OnAbilityPressed += OnAbilityPressed;
				_view.OnRestartPressed += RestartGame;

				_leftPlayer.AbilityExecuted += OnPlayerAbilityExecuted;
				_rightPlayer.AbilityExecuted += OnPlayerAbilityExecuted;
			}
		}

		private void OnDestroy()
		{
			if (gameObject != null && ClientManager.Instance != null)
			{
				ClientManager.Instance.SetEntity -= OnSetEntity;
				ClientManager.Instance.AbilitySelected -= OnAbilitySelected;
				ClientManager.Instance.NextTurn -= NextTurn;

				_view.OnAbilityPressed -= OnAbilityPressed;
				_view.OnRestartPressed -= RestartGame;

				_leftPlayer.AbilityExecuted -= OnPlayerAbilityExecuted;
				_rightPlayer.AbilityExecuted -= OnPlayerAbilityExecuted;
			}
		}

		private void OnSetEntity(EntityObject leftEntity, EntityObject rightEntity)
		{
			_leftPlayerAbilities = AbilityCreator.Create(ClientManager.Instance.Abilities);
			_rightPlayerAbilities = AbilityCreator.Create(ClientManager.Instance.Abilities);

			_leftPlayerAbilities.ForEach(x => x.Init(_leftPlayer, _rightPlayer));
			_rightPlayerAbilities.ForEach(x => x.Init(_rightPlayer, _leftPlayer));

			_leftPlayer.Init(_leftPlayerAbilities, leftEntity);
			_rightPlayer.Init(_rightPlayerAbilities, rightEntity);

			_view.Init(_leftPlayerAbilities);
			_view.gameObject.SetActive(true);

			StartBattle();
		}

		private void StartBattle()
		{
			_leftPlayer.gameObject.SetActive(true);
			_rightPlayer.gameObject.SetActive(true);

			NextTurn(_isLeftPlayerTurn);
		}

		private void OnAbilityPressed(string abilityName)
		{
			ClientManager.Instance.OnClientAbilitySelected(abilityName);
		}

		private void OnAbilitySelected(string abilityName)
		{
			var currentPlayer = _isLeftPlayerTurn ? _leftPlayer : _rightPlayer;
			currentPlayer.ExecuteAbility(abilityName);

			CheckPlayerHP();
		}

		private void OnPlayerAbilityExecuted(IAbility ability)
		{
			ClientManager.Instance.OnClientNextTurn(_isLeftPlayerTurn);
		}

		private void NextTurn(bool isLeftPlayerTurn)
		{
			_isLeftPlayerTurn = isLeftPlayerTurn;

			if (_isLeftPlayerTurn)
			{
				UpdatePlayer(_leftPlayer, _rightPlayer);

				_leftPlayerAbilities.ForEach(x =>
				{
					_view.UpdateCooldown(x.AbilityData.Name, x.Cooldown);
				});
			}
			else
			{
				UpdatePlayer(_rightPlayer, _leftPlayer);
			}

			_view.UpdateStateButtons(_isLeftPlayerTurn);

			CheckPlayerHP();
		}

		private void UpdatePlayer(IPlayer nextPlayer, IPlayer prevPlayer)
		{
			prevPlayer.EndTurn();
			nextPlayer.StartTurn();
		}

		private void RestartGame()
		{
			_leftPlayer.Restart();
			_rightPlayer.Restart();

			_leftPlayer.gameObject.SetActive(true);
			_rightPlayer.gameObject.SetActive(true);

			_view.gameObject.SetActive(true);
			_view.GameStart();

			NextTurn(true);
		}
		
		private void CheckPlayerHP()
		{
			if (_leftPlayer.CurrentHP == 0 || _rightPlayer.CurrentHP == 0)
			{
				GameOver();
			}
		}

		private void GameOver()
		{
			_leftPlayer.gameObject.SetActive(false);
			_rightPlayer.gameObject.SetActive(false);

			_view.GameOver();
		}
	}

}
