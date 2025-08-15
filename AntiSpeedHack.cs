using Sandbox;

public sealed class AntiSpeedHack : Component
{
	[Property, Group( "Anti-Speedhack Config" )]
	GameObject[] trackedPlayers = new GameObject[1];

	[Property, Group( "Anti-Speedhack Config" )]
	Vector3[] lastPositions = new Vector3[1]; // Set the player's current position before running to avoid false positives on first frame.

	[Property, Group( "Anti-Speedhack Config" )]
	float maxAllowedSpeed = 16f;

	protected override void OnFixedUpdate()
	{
		for ( int i = 0; i < trackedPlayers.Length; i++ )
		{
			if ( trackedPlayers[i] != null )
			{
				if ( (trackedPlayers[i].WorldPosition - lastPositions[i]).Length > maxAllowedSpeed )
				{
					Log.Warning( $"[AntiCheat] Player {trackedPlayers[i].Name} exceeded speed limit!" );
					trackedPlayers[i].WorldPosition = lastPositions[i];
				}

				lastPositions[i] = trackedPlayers[i].WorldPosition;
			}
		}
	}
}
