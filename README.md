# phoneTool
Phone Number Tool that is searchable using a textfile to populate the DataView

I created this at my workplace so that i could search all of the numbers to find specific ones.

This tool does the following:

1. populates the DataGridView from a .csv file
2. checks a github repo that hosts the updated .csv and compares the file hashes.
   Then replaces the existing if newer.
3. upon first launch, creates its own folder in local app data and gets the newest .csv, allowing the exe to be ran from anywhere.
