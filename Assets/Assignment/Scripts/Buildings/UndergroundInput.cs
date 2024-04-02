using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundInput : Conveyor
{

}

/*

UndergroundConveyor.cs
- 1 tile per end (input and output)
- Input has an input, output has an output (shocking) and inputs on the sides
- Allows products to pass under other buildings and conveyors
- Split into two buildings (unlocked at the same time)
- Input end inherits from Conveyor with different behaviour for transporting (has no output)
- Output end inherits from Conveyor (standard conveyor)
- Input end overrides:
  - Variables: transportDelayPerTile, maxTilesBetweenEnds
  - override fn GetTransportTime() => gets the current output (GetOutput(out distance)), and multiplies the distance by transportDelayPerTile
  - override fn HasValidOutput() => returns whether or not GetOutput() is null and whether that output is accepting input
  - fn GetOutput(out distance) => walks forward for maxTilesBetweenEnds + 1 (to include the output), checking for an output tile facing the correct direction. returns it if found, null otherwise

*/
