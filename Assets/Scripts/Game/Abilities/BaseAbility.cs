using Project;
using Project.Ability;
using Project.Player;

public abstract class BaseAbility : IAbility
{
	protected AbilityData _abilityData = null;

	protected int _currentCooldown = 0;

	protected IPlayer _affectedPlayer, _otherPlayer = null;

	public bool IsReady => _currentCooldown == 0;
	public int Cooldown => _currentCooldown;

	public int Durability => _abilityData.Durability;

	public AbilityData AbilityData => _abilityData;

	public abstract void Execute();

	public void Bind(AbilityData abilityData) => _abilityData = abilityData;

	public abstract void Init(IPlayer ownerPlayer, IPlayer enemyPlayer);

	public virtual void DecreaseCooldown()
	{
		if (_currentCooldown > 0)
		{
			_currentCooldown--;
		}
	}

	public virtual void Reset()
	{
		_currentCooldown = 0;
	}
}
