# Pencil Durability [![Build Status](https://travis-ci.org/Agies/Pencil-Durability-CSharp.svg?branch=dev)](https://travis-ci.org/Agies/Pencil-Durability-CSharp)

## Requirements
1) Install [Dotnet Core 2.*](https://dotnet.microsoft.com/download)
1) To Start Review
    1) Test `ci/test.bat` or `ci/test.sh`
    1) Run `ci/run.bat` or `ci/run.sh`
1) Review

## Notes
* The Run experience demonstrates the different capabilities of the Pencil and Paper 
* The IDE I used was Rider, in my quick testing VS did not seem to have any issues
* I tested the Run and Test on Windows and Mac, if you are reviewing on Linux it should work
* I had a question about pencil degradation during editing, I added a note, but did not implement it
since the story did not explicitly say editing was a full write action and I don't have a PO to question.
However, I did implement it just in case on a separate branch `git checkout feature/editing_should_degrade`. I made one assumption about
the @ symbol being a standard character and not an uppercase character.  
