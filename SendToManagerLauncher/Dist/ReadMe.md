## SendtoTools
SendtoTools can add various tools on "SendTo" menu of the explorer.
It can also manage these tools.

### Features
* You can manage your favorite tools by categolized by "Inventory".
* You can sort tools by putting numbers at the head of tool name.
* This application can be portable.
* You can put your tools on "Sendto" when you need them and remove them when it is no longer needed.

## Download
Download binary from https://github.com/ambiesoft/SendToTools/releases

## Environment
.Net4 and Visual Studio 2013 runtime library are required.

## Install
Extract downloaded archive and lanucn '''SendToManager.exe'''.

## Basic usage

1. Extract a downloaded archive and launch *SendToManager.exe*.
2. Create new shortcut files by clicking **New Item** or copy shortcut files to the Inventory folder which can be opened by selecting **[Folder] > [Open Current Inventory]**.
3. You can reoder items by **dragging-and-dropping** or clicking **[up] or [donw]** button on the toolbar.
4. You can put number by the oder of the list view by clicking **[Number]** button. This makes the oder of the menus of **SendTo** equal to the oder shown in the list view.
5. By clicking **[Deploy]**, All shortcut files will be copied on the **SendTo Folder**.
6. By clicking **[Displace]**, All shortcut files in **SendTo Folder** will be removed.

## Advanced usage
1. You can create multiple inventories.

## Command line options
Following commnad will deploy the inventory named *MyInventory*. Additional option "-y"makes no confirmation dialog appear.

```SendToManager.exe --apply MyInventory -y```




## Tools
Basically you can put any shortcut files on "SendTo", but SendtoTools also ships with various tools.

#### ChangeFileName
This tool can change file name or move file with the name you specified. One of the benefit of this tools is that if you want to change the file name as a part of the content of the file, you can paste the content into this app before closing app, then after closing the app, renaming operation can be performed.

#### ChangeFileTime
You can change file time (Creation time, Last Modified time and Last Access time).

#### CopyFileContent
Copy file content onto the clipboard. Only Text file and Image file can be copiable.

#### CopyPath
Copy path of the file. You can show a dialog by pressing shift, ctrl or RightButton when launching the app. With this dialog, more customized form of path can be copiable.

#### DotNet4Runnable
This tool creates .config file so that .Net2.0 application can be runnable on .Net4.0 platform. If the environment does not have .Net2.0 installed and you want to run .Net2.0 application, you can send .exe to this tool to create .config.

#### Move to
Move file(s) to favorites location.

#### CommandPrompt
Open Command Prompt as current directory is a path sent to this app.

#### RegexFileRenamer
Rename file by a regular expression.

#### RunFileAs
Run file as administrator.

#### Switch3264
You need specify two option, "-32" and "-64". When launched in 32bit enviromment, the argument of "-32" will be launched. Same will do in 64bit.

#### touch
Change file time(Last Access time and Last Write time) as current time. If you launch with shift,control or rbutton key pressed, it touches all files in subdirectories.


## License
This software is freeware, See Copying.txt


## Uninstall
Remove extracted files.


## Support
If you have troubles, post *Issue* on <https://github.com/ambiesoft/SendToTools/issues>.

## Contact
- Author: Ambiesoft trueff
- E-mail: <ambiesoft.trueff@gmail.com>
- Webpage: <http://ambiesoft.fam.cx/main/index.php?page=sendtotools>
- Forum: <http://ambiesoft.fam.cx/minibbs/minibbs.php>
- Development: <https://github.com/ambiesoft/SendToTools>

