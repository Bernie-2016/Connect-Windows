# Movement for Windows

Welcome to the repository for the Movement app for Windows (formerly known as BernieApp).  This repository consists of a .Net solution that contains projects for 
[Windows 10 (UWP)](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp.UWP) and [Windows Phone 8.1](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp/BernieApp.WindowsPhone).  All of these projects depend on the [BernieApp.Common](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp.Common) Portable Class Libarary which contains the business logic for the solution.

## Setting up your build environment

First you will need to install Visual Studio 2015 Community edition. Clone the repository. Once the repository is cloned on your local machine, open it up in Visual Studio and Rebuild the solution. This will download all necessary Nuget packages. To work on the Windows 10 UWP version, you will need to be on Windows 10 with all of the latest updates, as well as have the latests Windows 10 SDK. Likewise, for the Windows Phone 8.1 app, you will need to be on atleast Windows 8.1 (10 works just fine, of course!) with the WinRT SDK installed. Missing an SDK? Run the Visual Studio Installer and select 'Modify', then check the necessary components. Visual Studio should notify you of any of the above items missing when you attempt to open the projects.

## Contributing

We welcome all contributions.  If you see a bug please report it in using the Issues feature.  If you would like to help develop the Movement app for Windows applications you can contact the CodersForSanders group using our Slack channel (you can request access [here](https://docs.google.com/forms/d/1pmxGTX17qPkZV49iuLh3rN-Mj_Z6w6M_XtUJMZCMIP4/viewform)): https://codersforsanders.slack.com/messages/bernie-app/details/

We will be tracking development progress in our [Pivotal project](https://www.pivotaltracker.com/n/projects/1530927)

## What we use

* C#/XAML
* Mvvmlight (an MVVM framework)
* Json.NET (for interacting with json documents)
* Template10 (UWP only)

### Rules of the road for software changes

* When making the changes create a **feature** branch by branching off the most recent version of **master**.
* When your change is ready, first merge it in to the **develop** branch and make sure the change plays nicely with the current work in progress.
* Assuming nothing went boom, create a pull request to merge your feature branch in to **master**.
* Once your branch gets successfully merged to **master** through the pull request you can delete your **feature** branch.
* Don't resurrect old branches if a bug is found, create a new issue for the bug and thus a new branch to fix it.
* We will NEVER merge **develop** to **master**.  **develop** should be able to be deleted at any time and be recreated by branching off of **master**.  It is your **feature** branches that will be merged in to master.  So if you find a bug after you merge to **develop** remember to fix it in your **feature**, not **develop** or your fix won't be seen in the pull request.

Feel free to reach out to the team on Slack under the @Movement-windows-app channel!


