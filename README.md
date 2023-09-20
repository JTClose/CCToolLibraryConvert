# CCToolLibraryConvert
Carbide Create Tool Library  Edit and Convert
CCToolLibraryConvert

.Net 6.0
Windows 7 target OS
Written on Windows 10, Visual Studio 2022 Community

NewtonSoft DLL's included for Fusion 360 files ( future ).
HSM*xml files are part of that future 

The intent is to be able to handle Fusion 360 Tool libraries concurrently in the future, hence "Convert" in the program name.

This program currently reads a Carbide Create Tool library exported as a CSV file.
The program is an editor for multiple CSV files.
You are allowed to copy data between files, add new and delete.

The Status bar at the bottom shows
	Message
	Number of rows selected in the ListView
	Number of items on the clipboard
	Current file name
	Number of Tools in current file
	Number of open files

The TabStrip represents the all the open files which are represented by the Tab pages
	The Tab page text will be the File name.  This will be pre-pended with an "* " if there have been changes.
	The current Tab page text will be bold.
	Selecting the Tab page with change the current file.
	Selecting the X on the Tab page will delete the file from the Session
		If the file has changes, you will be prompted to Ignore changes or Save
		
The ListView displayes and allows navigation/selection of rows
	Mulitple row Selection are allowed.
	Right Click the selected rows
		To access the 
		To Select All/UnSelect 
	Selecting the Column Headers will sort the data
	You may change the order/width of the Columns.
		You must Save the layout using the Settings menu
	
The PropertyGrid is used to edit the data
	Any selected row will populate the Grid
	Selecting multiple rows in the ListView will make the PropertyGrid display common data where appropriate
	Edits in the PropertyGrid are not applied until the Apply button is selected.

Files menu
	New
		New file with no rows.   You will be forced to save as new name.
	Open
		Single or Multiple files
			Last file opened will be the current file
			Last folder accessed will be saved in .config file
	Save
		Current
			The current file will be saved using the same name.
			Any "change" flags are removed
		All changed files
			All files in the current Session are checked for change flags
				If found the file will be saved using the same name				
	Close All
		All files in the current Session are closed.
			For each file, if change is set, your are prompted to Ignore or Save
	Exit
		Same as Close All
		Exits the application.
	
	Note:
	File overwrites are checked and reported during Open/Save dialog boxes.
	System error events are reported for locked files and whatever else.
	
Settings menu
	ColumnLayout
		Save the layout.  This is stored in the.config fileC
		Clear Layout.  The program will use the default data order and calculate a minimum width per parameter name ( not data )

User config file contains
	Window Location
	Window Size
	Splitter location
	LastJsonFolder used
	LastCSVFolder used
	ListView ColumnLayout



Limitations !!!!

There is NO limits as to what data is allowed for a parameter other than by data type.
NO parameter calculations
Be careful

No testing on dual screen PC devices.


