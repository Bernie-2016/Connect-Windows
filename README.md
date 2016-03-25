# Connect for Windows

Welcome to the repository for the Connect app for Windows (formerly known as Movement).  This repository consists of a .Net solution that contains projects for: 
* [Windows 10 Desktop and Mobile (UWP)](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp.UWP)
* [Windows Phone 8.1](https://github.com/SandersForPresident/BernieAppWindows/tree/master/BernieApp/BernieApp.WindowsPhone)
* [BernieApp.Portable](https://github.com/Bernie-2016/Connect-Windows/tree/master/BernieApp.Portable), a Portable Class Libarary which contains the data access layers and models
* A [Command line project](https://github.com/Bernie-2016/Connect-Windows/tree/master/BernieApp.CLI) for command-line testing

We use the MVVM (Model-View-ViewModel) pattern in development of these applications. ViewModels and Views are located within both the UWP and Windows Phone 8.1 project directories due to the differing navigation and UI reqirements for each platform, while the Models and data access code are located in the PCL. We use Dependency Injection, IoC, Messaging, and other MVVM features.

## Setting up your build environment

First you will need to install Visual Studio 2015 Community edition. Clone the repository. Once the repository is cloned on your local machine, open it up in Visual Studio. Clean and Rebuild the solution which will download all necessary Nuget packages. To work on the Windows 10 UWP version, you will need to be on Windows 10 with all of the latest updates, as well as have the latest Windows 10 SDK. Likewise, for the Windows Phone 8.1 app, you will need to be on atleast Windows 8.1 (10 works just fine, of course!) with the WinRT SDK installed. Missing an SDK? Run the Visual Studio Installer and select 'Modify', then check the necessary components missing from your installation.

## Contributing

We welcome all contributions.  If you see a bug please report it in using the Issues feature.  If you would like to help develop the Connect app for Windows applications, please join the Connect with Bernie Slack channel (you can request access [here](http://connectwithbernieslack.herokuapp.com/)). Be sure to message @rileytaylor once inside to gain access to the relevent channels.

We will be tracking development progress in our [Pivotal project](https://www.pivotaltracker.com/n/projects/1530927)

### How to contribute

* Fork the Repository
* Mark what you are working in inside the Pivotal project
* Discuss changes with the team on Slack
* Make your changes and ensure that they build and nothing breaks
* Create a pull request on the main repository

## What we use

* C#/XAML
* Mvvmlight (an MVVM framework)
* Json.NET (for interacting with json documents)
* Template10 (UWP only, a UI framework with MVVM enhancements)
