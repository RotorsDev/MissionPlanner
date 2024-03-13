# Protar flight log analysis

1. Extract flight `.BIN` file from autopilot SD card
   - Alternatively, use Mission Planner to download the `.BIN` file from the autopilot
     - Mission Planner &rarr; **Flight Data** window &rarr; Left panel under the HUD &rarr; **DataFlash Logs** tab &rarr; **Download DataFlash Log Via Mavlink** button
2. Convert `.BIN` file to `.log` using Mission Planner
   - Mission Planner &rarr; **Flight Data** window &rarr; Left panel under the HUD &rarr; **DataFlash Logs** tab &rarr; **Convert .Bin to .Log** button
3. Use [log_separator.py](log_separator.py) to pick out the GPS lines from the log where the vehicle was flying in Auto mode
   - Each section will be saved in a seperate file with the same name as the original log file, but with an additional index (`00000001.log` &rarr; `00000001_1.log`, `00000001_2.log`, ...)
