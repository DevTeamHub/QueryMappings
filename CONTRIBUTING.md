How to contribute

One of the easiest ways to contribute is to participate in discussions and discuss issues. You can also contribute by submitting pull requests with code changes.

General feedback and discussions?

Please start a discussion on the Home repo issue tracker.

Bugs and feature requests?

For non-security related bugs please log a new issue in the appropriate GitHub repo. Here are some of the most common repos:

///

Reporting security issues and bugs

Security issues and bugs should be reported privately, via email, to the Microsoft Security Response Center (MSRC) secure@microsoft.com. You should receive a response within 24 hours. If for some reason you do not, please follow up via email to ensure we received your original message. Further information, including the MSRC PGP key, can be found in the Security TechCenter.

Other discussions

Our team members also monitor several other discussion forums:

ASP.NET Core forum
StackOverflow with the asp.net-core, asp.net-core-mvc, or entity-framework-core tags.
JabbR chat room for real-time discussions with the community and the people who work on the project
Filing issues

When filing issues, please use our bug filing templates. The best way to get your bug fixed is to be as detailed as you can be about the problem. Providing a minimal project with steps to reproduce the problem is ideal. Here are questions you can answer before you file a bug to make sure you're not missing any important information.

Did you read the documentation?
Did you include the snippet of broken code in the issue?
What are the EXACT steps to reproduce this problem?
What package versions are you using (you can see these in the project.json file)?
What operating system are you using?
What version of IIS are you using?
GitHub supports markdown, so when filing bugs make sure you check the formatting before clicking submit.

Contributing code and content

You will need to sign a Contributor License Agreement before submitting your pull request. To complete the Contributor License Agreement (CLA), you will need to submit a request via the form and then electronically sign the Contributor License Agreement when you receive the email containing the link to the document. This needs to only be done once for any .NET Foundation OSS project.

Make sure you can build the code. Familiarize yourself with the project workflow and our coding conventions. If you don't know what a pull request is read this article: https://help.github.com/articles/using-pull-requests.

Before submitting a feature or substantial code contribution please discuss it with the team and ensure it follows the product roadmap. You might also read these two blogs posts on contributing code: Open Source Contribution Etiquette by Miguel de Icaza and Don't "Push" Your Pull Requests by Ilya Grigorik. Note that all code submissions will be rigorously reviewed and tested by the ASP.NET and Entity Framework teams, and only those that meet an extremely high bar for both quality and design/roadmap appropriateness will be merged into the source.

Here's a few things you should always do when making changes to the code base:

Engineering guidelines

The coding, style, and general engineering guidelines are published on the Engineering guidelines page.

Commit/Pull Request Format

Summary of the changes (Less than 80 chars)
 - Detail 1
 - Detail 2

Addresses #bugnumber (in this specific format)
Tests

Tests need to be provided for every bug/feature that is completed.
Tests only need to be present for issues that need to be verified by QA (e.g. not tasks)
If there is a scenario that is far too hard to test there does not need to be a test for it.
"Too hard" is determined by the team as a whole.