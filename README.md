# Pencil Durability

## Requirements
1) Install [Dotnet Core 2.*](https://dotnet.microsoft.com/download)
1) To Start Review
    1) Run `ci/run.bat` or `ci/run.sh`
    1) Test `ci/test.bat` or `ci/test.sh`
1) Review

## Notes
* Editor used Rider, in my quick testing VS did not seem to have issues
* I tested the Run and Test on Windows and Mac, if you are reviewing on Linux it should work
* For a more interesting Run experience `git checkout introduction_game`
* I had a question about pencil degradation during editing, I added a note, but did not implement it
since the story did not explicitly say editing was a full write action and I don't have a PO to question.
However, I did implement it just in case `git checkout editing_should_degrade`. I made one assumption about
the @ symbol being a standard character and not an uppercase character.  
