# sbox-anti-speedhack

A simple script to block speedhacks in s&box.

## How it works
Checks player movement speed every tick.  
If a player is faster than allowed, their position resets to the last valid one and a warning is shown.

## Setup
1. Add `AntiSpeedHack.cs` to your project.
2. Attach it to a game object.
3. In inspector, set:
   - **TrackedPlayers** — who to monitor  
   - **LastPositions** — starting positions  
   - **MaxAllowedSpeed** — speed limit

## Code
```csharp
using Sandbox;

public sealed class AntiSpeedHack : Component
{
	[Property, Group( "Anti-Speedhack Config" )]
	GameObject[] trackedPlayers = new GameObject[1];

	[Property, Group( "Anti-Speedhack Config" )]
	Vector3[] lastPositions = new Vector3[1];

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
