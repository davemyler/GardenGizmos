# GardenGizmos SLMM

## Assumptions
- The length of the lawn is in the north-south axis
- The width of the lawn is in the east-west axis
- Lawn coordinate 0/0 is in the south-west corner of the lawn
- Attempting to move forwards when facing the edge of the lawn does nothing, and does not incure the time normally taken to move.

## Build and Run

The GardenGizmos.SLMM solution can be built and run standalone. It will start a console
app that exposes an API on localhost:5000. The following endpoints are available:

- /api/mowingmachine/position. This will return a JSON document showing the current position. The initial position is
  set at 0,0 (south-west corner) facing North.
- /api/mowingmachine/turn-clockwise. Instructs the machine to turn clockwise.
- /api/mowingmachine/turn-anticlockwise. Instructs the machine to turn anticlockwise.
- /api/mowingmachine/move-forwards. Instructs the machine to move forwards.

All requests except /position are queued and will be executed in turn. Movements
take 5s and turns take 3s, and the position will not be updated until after the command is
complete.

The /position request can be made at any time and indicates the current position (starting
position if a command is in progress), regardless of any queued commands.

## Tests

The GardenGizmos.SLMM.Tests project contains a single automated test that exercises
the machine and tests boundary conditions. A single full-stack test is used because 
the API is stateful is respect of the machine.
