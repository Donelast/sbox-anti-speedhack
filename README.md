# sbox-anti-speedhack

Simple server-side speedhack detection for s&box.

## How it works
The script checks player movement speed every fixed update.  
If a player moves faster than the allowed limit, their position is reset to the last valid one and a warning is logged.

## Usage
1. Copy `AntiSpeedHack.cs` to your project.
2. Add the component to an object in your scene.
3. Set up in the inspector:
   - `TrackedPlayers` – list of players to monitor
   - `LastPositions` – last known positions (set before starting)
   - `MaxAllowedSpeed` – speed limit

## Code
```csharp
using Sandbox;
using System;
using static Sandbox.VertexLayout;

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
