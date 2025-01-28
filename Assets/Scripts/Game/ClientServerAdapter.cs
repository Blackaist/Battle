using Project;
using Project.Ability;
using System.Collections.Generic;

public class ClientServerAdapter : IClientServerAdapter
{
	private ClientManager _client = null;
	private Server _server = null;

	public ClientServerAdapter(ClientManager client)
	{
		_client = client;
		_server = new Server(this);
	}

	public bool IsServerLoaded => _server.IsCompleted;

	public void LoadAssets()
	{
		_server.LoadAssets();
	}

	public IReadOnlyCollection<EntityObject> Entities() => _server.Entities;
	public IReadOnlyCollection<AbilityData> Abilities() => _server.Abilities;
	public IReadOnlyCollection<EffectData> Effects() => _server.Effects;

	public void OnClientSelectedEntity(EntityObject entity)
	{
		_server.TrySelectEntity(entity);
	}

	public void OnServerSetEntity(EntityObject leftEntity, EntityObject rightEntity)
	{
		_client.SetEntity(leftEntity, rightEntity);
	}

	public void OnClientAbilitySelect(string ability)
	{
		_server.OnSelectAbility(ability);
	}

	public void OnServerAbilitySelect(string ability)
	{
		_client.OnServerAbilitySelected(ability);
	}

	public void OnClientNextTurn(bool isLeftTurn)
	{
		_server.OnNextTurn(isLeftTurn);
	}

	public void OnServerNextTurn(bool isLeftTurn)
	{
		_client.OnNextTurn(isLeftTurn);
	}
}
