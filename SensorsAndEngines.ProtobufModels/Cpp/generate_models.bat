@echo off
ping -n 10 127.0.0.1 >nul
nanopb_generator %~2 --output-dir="%~1" --options-file=models.options models.proto
