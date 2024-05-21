## SendtoTools
SendtoTools is an application that adds and manages various items to the "Send" menu of Explorer.

### Features
* You can register and manage your favorite tools in the "Inventory".
* The tool's file names can be numbered and sorted.
* This application can be portable.
* You can register in the "Send" menu only when necessary, and then cancel it.

## Download
Download binary from https://github.com/ambiesoft/SendToTools/releases

## Environment
.NET Framework4.8 and Visual Studio 2022 runtime library are required.

## Install
Extract the downloaded archive and launch '''SendToManager.exe'''.

## Basic usage

1. Unzip the downloaded archive and start *SendToManager.exe*.
2. Create new shortcut files by clicking **New Item** or copy shortcut files to the **Inventory** folder which can be opened by selecting **[Folder] > [Open Current Inventory]**.
3. You can reoder items by **dragging-and-dropping** or clicking **[up] or [down]** button on the toolbar.
4. You can put ordered numbers by clicking **[Number]** button. This makes the order of **SendTo** menu same as this order.
5. By clicking **[Deploy]**, All shortcut files will be copied onto the **SendTo Folder**.
6. By clicking **[Displace]**, All shortcut files in **SendTo Folder** will be removed.

## Advanced usage
1. You can create multiple inventories.

## Command line options
Following commnad will deploy the inventory named *MyInventory*. Additional option "-y" shows no confirmation dialog.

```SendToManager.exe --apply MyInventory -y```

## Tools
Any shortcut files can be put on "SendTo". But SendtoTools comes with the following tools.

#### ChangeFileName
This tool can change file name or move the file with the name you specified. One of the benefit of this tools is that if you want to change the file name but it is locked by a process from which you want to get name information, you can paste the name into this app before closing app, then after closing the app, renaming operation can be performed.

#### ChangeFileTime
You can change the file time (Creation time, Last Modified time and Last Access time).

#### CopyFileContent
Copy a file content onto the clipboard. Only Text file and Image file can be copiable.

#### CopyPath
Copy the path of the file. This shows a dialog when launch with pressing shift, ctrl or RightButton. With this dialog, more customized form of path can be specified.

#### DotNet4Runnable
This tool creates ".config" file so that .Net2.0 application can be runnable on .Net4.0 platform. If the environment does not have .Net2.0 installed and you want to run .Net2.0 application in .NET4 environment, you can send the executable to this tool to create .config.

#### Move to
Move file(s) to favorites location.

#### CommandPrompt
Open a Command Prompt with current directory as the directory of the file or folder.

#### RegexFileRenamer
Rename files using a regular expression.

#### RunFileAs
Run a file as administrator.

#### touch
Change the file time(Last Access time and Last Write time) to current time. If you launch with shift,control key or R-Button pressed, it touches all files in subdirectories.


## License
This software is freeware, See Copying.txt


## Uninstall
Remove extracted files.


## Acknowledgment
RegexFilenameRenamer uses Glob.cs <https://github.com/mganss/Glob.cs>.

## Support
If you have troubles, post *Issue* on <https://github.com/ambiesoft/SendToTools/issues>.

## Contact
- Author: Ambiesoft trueff
- E-mail: <ambiesoft.trueff@gmail.com>
- Webpage: <https://ambiesoft.com/homepage/app/SendToTools>
- Forum: <https://ambiesoft.com/minibbs/minibbs.php>
- Development: <https://github.com/ambiesoft/SendToTools>

