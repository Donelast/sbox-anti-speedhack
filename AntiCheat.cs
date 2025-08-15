using Sandbox;
using System;
using static Sandbox.VertexLayout;

public sealed class AntiCheat : Component
{
	[Property, Group( "Anti-Cheat Config" )]
	GameObject[] trackedPlayers = new GameObject[1];

	[Property, Group( "Anti-Cheat Config" )]
	Vector3[] lastPositions = new Vector3[1]; // Important: in the inspector, set the player's current position before running, or you'll get a false trigger on the first frame.

	[Property, Group( "Anti-Cheat Config" )]
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
