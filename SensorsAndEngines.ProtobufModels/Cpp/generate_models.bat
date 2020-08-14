@echo off
set initial_dir=%1
nanopb_generator %2 --output-dir=%initial_dir% --options-file=models.options models.proto
