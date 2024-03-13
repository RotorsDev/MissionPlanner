source_file = "00000017"    # .log file name
add_headers = True          # Add encompassing MODE lines

current_file = None
file_index = 0

with open(f"{source_file}.log", "r") as sourceFile:
    for line in sourceFile:

        # MODE (...) Auto -> Start new file (increment counter)
        if line.startswith("MODE") and line.count(", Auto, ") > 0:
            file_index += 1
            file_name = f"{source_file}_{file_index}.log"
            current_file = open(file_name, "a")
            if add_headers:
                current_file.write(line)

        # MODE (...) Anything -> Close file
        if line.startswith("MODE") and current_file is not None and line.count(", Auto, ") == 0:
            if add_headers:
                current_file.write(line)
            current_file.close()
            current_file = None

        # GPS (...) -> Add to current file
        if line.startswith("GPS") and current_file is not None:
            current_file.write(line)
