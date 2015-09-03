# Bernie App for Windows

Welcome to the repository for the Bernie App for Windows.  This repository is consists of a .Net solution that contains projects for the [Windows Store 8.1](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp/BernieApp.Windows), [Windows Phone 8.1](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp/BernieApp.WindowsPhone), and [Wpf](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp.Wpf) platforms.  All of these projects depend on the [BernieApp.Common](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp.Common) Portable Class Libarary which contains the business logic for the solution.

## Setting up your build environment

First you will need to install Visual Studio 2015 Community edition or better (VS 2013 may work as well).  Then clone the repository.  Once the repository is cloned on your local machine you should be able to open up the solution in Visual Studio and a Rebuild of the solution will download all the necessary packages from nuget.

## Contributing

We welcome all contributions.  If you see a bug please report it in using the Issues feature.  If you would like to help develop the Bernie App for Windows applications you can contact the CodersForSanders group using our Slack channel (you can request access [here](https://docs.google.com/forms/d/1pmxGTX17qPkZV49iuLh3rN-Mj_Z6w6M_XtUJMZCMIP4/viewform)): https://codersforsanders.slack.com/messages/bernie-app/details/

### Rules of the road for software changes

* For every change create an issue in Github under this repository if one doesn't already exist
* When making the changes create a feature branch by branching off the most recent version of master
* Name your feature branches with the pattern User#IssueNumber optionally followed by brief description. E.g. "fyndor#1 Tesing out issue linking".
* When your change is ready first merge it in to the develop branch and make sure the change plays nicely with current work in progress and work that has be merged to master since you first started your branch
* Assuming nothing went boom create a pull request to merge your feature branch in to master
* Once your branch gets successfully merged to master through the pull request you can delete your feature branch
* Don't resurrect old branches if a bug is found, create a new issue for the bug and thus a new branch to fix it  

I am not trying to be super strict and if that seems too rigid come talk to me in Slack.  That is just how I like to work with my team at my job and I figured it would work well in this project as well. (@fyndor on Slack)