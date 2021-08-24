# phoneTool
Phone Number Tool that is searchable using a textfile to populate the DataView

I created this at my workplace so that i could search all of the numbers to find specific ones.

This tool does the following:

1. populates the DataGridView from a .csv file
2. checks a github repo (https://github.com/dirtydanisreal/numberData) that hosts the updated .csv and compares the file hashes.
   Then replaces the existing if newer.
3. upon first launch, creates its own folder in local app data and gets the newest .csv, allowing the exe to be ran from anywhere.

![Screenshot 2021-08-24 014020](https://user-images.githubusercontent.com/10038465/130562119-c7d25174-6476-48ef-afb3-8cb5d57328c2.png)

