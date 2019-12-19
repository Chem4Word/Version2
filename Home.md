# Version 2.0.1.0 Available!

Features of this release are:-
* Uses the Open Office XML to render structures in publication ready quality when used with Word 2010 or later.
* Added user option to disable attaching of Chem4Word template (to preserve user macros and styles etc).  This also disables gthe gallery.
* Editor form allows reading and writing of CML files and MDL Molfiles.
* Fixed bug in saving charges of atoms.
* Improved startup (better error handling).
* Fixed issue with parsing of doubles when culture is not US English.
* Fixed issue where Word asks to save every document even if no changes done, when add-in enabled.
* Automatic check for new version
* Synchronise options for editor and rendering
* New draw structure button
* Updated ChemDoodle Web to 7.0.2
* Fix bug in Un-Install which caused "C:\Users\{UserName}\AppData\Local\Chemistry Add-in for Word" folder to be deleted

# Version 1.6.1 Released!

* Added ability to add or remove explicit hydrogens.
* Forced drawing of explicitly drawn hydrogens.
* Fixed bug in JSON code.

# Introduction 
The **Chem4Word Project** began in 2008 as a collaboration between Microsoft Research  and the University of Cambridge, designed to make it easier to insert and modify chemical information (labels, formulas, 2-D depictions, etc.) from within Microsoft Office Word, and also to have the chemical information stored and manipulated in a semantically rich manner. 

On March 22, 2010, at the ACS meeting in San Francisco, CA, we announced the availability of a beta build, and we are now launching Chem4Word as an open source project overseen by Dr Joe Townsend. 

# Contacting The Chem4Word Team
The best way to get in contact with us and to bring up issues or bugs is via the GitHub. 
Join our group on [facebook](http://www.facebook.com/home.php?sk=group_186300551397797&ap=1).

This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/) to clarify expected behavior in our community. For more information see the [.NET Foundation Code of Conduct](http://www.dotnetfoundation.org/code-of-conduct).

# Future work
We have identified a number of feature areas where we have definite plans for taking the project forward – these fall into two categories; chemical and non-chemical - each of the areas are outlined below in no particular order. In some cases we have already made a start on the code but these are not yet in a working state; in others we have fairly clear ideas as to how we want to progress and we hope that if we provide a very skeleton proof of concept implementation then people with more experience or design skills than us could take them forward. Finally some of the tasks need agreement at a high level as to the direction we should move in and we are engaging with the relevant people and organisations to proceed.  We would welcome your feedback in these areas.
* **Chemical** 
	* 2D layout –if a molecule does not have a complete set of x2 and y2 coordinates on all the atoms then currently we cannot show a 2D representation. We plan implement the algorithm described by Clark et al. in J. Chem. Inf. Model., 2006, 46, 1107-1123 which will allow us to generate 2D representations from the connectivity. Of course as we want to support both high school and university chemistry we will probably need two different layout programs; one for each style. Should the bond angles be 120° or 90°?
	* 3D display – there is no 3D viewer in the add-in at present and we want one. 
	* Converters – at the moment only CML and mol can be imported – but if we have converters then chemdraw, marvin, spectra (JCAMP) etc. could be imported. 
	* PeakList UI – how can a user enter spectral data and associate particular stretches or chemical shifts with bonds and atoms? We have some UI mock-ups already prepared and are working on how the backing CML should hold the data. 
	* Styles – since the start of the project we have intended to create chemical style sheets; several of our design decisions have been taken deliberately with ChemSS in mind even though so far we haven't even the beginnings of an implementation. Styles would need to be able to hold everything from preferred bond lengths and angles to whether or not methyl groups should be represented as "Me", "CH3" or just a line etc. 
	* Reactions – we want to support these but how should we. A big area.
* **Non chemical**
	* Test environment – we have a set of unit tests for NUMBO but need to fully test all the code, we want a continuous build and test setup. We are looking at maven at the moment. 
	* GUI testing – we have a fair amount of GUI and it needs to be tested. 
	* Command Window – using COA we should be able to perform all the chemical changes which are taking place in the 2D editor and elsewhere; this means we should be able to script things through a command window – possibly used to test as well. 
	* Copy/Paste – an implementation that fully works
	* Gallery – the gallery uses chem4word.dotx to store molecules which the users places in the gallery. This template is based on normal.dotx and therefore installing the add-in can mess up peoples' formatting. It would be better to simply point to a directory and when view gallery is pressed to show a navigator-like view of all the molecules in this directory. The user can use this to select the molecule in insert.
	* Smart Tag – similar changes to the gallery required. Lets pick up the molecules the user already has in their molecule directory.
	* Math Zones – the add-in wraps math zones to show chemical text in the document but we have to use different techniques to show chemical text when not in the document. This gives an inconsistent look and feel (and means that we have to maintain more code). Either we should be using math zones everywhere (which many not be possible) or nowhere. Wrapping the math zones is also (we believe) causing problems with allowing us to do direct editing of the chemistry in the document, rather than having to use modal dialogues.
	* Ribbon – The ribbon currently uses customUI which is preventing us from having buttons with dynamic content; and we really want these. 
	* Hooks into CML – how should third parties get hold of the CML? And how could they make changes and then give it back to us again which some changes? 
	* RACSO peak lists – the idea behind this is to be able to convert the spectral data entered by the user into the form currently accepted by the publishers (each publisher has a particular and slightly different house style) in the correct format and insert it into the document. Changing the ChemSS would change the formatting. 
	* Functional programming and performance - the code has been written in a stateless manner which provides an elegant design but is not optimised for performance. This is entirely in keeping with Donald Knuth's "premature optimization is the root of all evil". However Chem4Word has some areas (systems with many rings, interactive drawing) where many identical calculations are repeated and this is evident in performance). This is an area where modern programming methods (e.g. memoization) should be rewarding.

Since our beta release in March 2010 we have been making several usability improvements including an improved 2D editor, some bug fixes, and also a completely refactored codebase.  The package names have been changed to better reflect what they are doing, we have added new packages and we have moved various pieces of code (for example the navigator) from one package to another.

Copyright (C) .NET Foundation. All rights reserved. This code is licensed under the Apache License, Version 2.0. THIS CODE IS PROVIDED **AS IS** WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

# History

# Version 1.5 Now Fully Released!
After a successful beta, **Version 1.5 is now ready for download**.  This is the **full release version**, made possible by our wonderful team (Andy Wright, Eric Schultz, Mike Williams, Alex Wade and Clyde Davies) and by the splendid people at [ChemDoodle](https://www.chemdoodle.com).  

Version 1.5 addresses one of the most common requests for enhancements:  the ability to create your own chemical structures from scratch.  We integrated the publically available version of the ChemDoodle editor to allow you to do this.  

We also have better integration with [PubChem](https://pubchem.ncbi.nlm.nih.gov) services.  We have fixed a bug on querying the service, allowed paging through results, and also have sorted the results by relevance.

Please feel free to download!

# Version 1.5 Beta
**Great news!** We now have the **beta** of the next version of Chem4Word ready for testing! We need **you** to test it. The most exciting new feature is integration with the ChemDoodle editor: you can now create your own chemical structures from scratch. There is also improved access to online repositories of chemical structures.
 
# Version 1.1 Release
This release is the first release under the aegis of the Outercurve Foundation .  It is fully compatible with Word 2013 and also fixes some issues with OPSIN and PubChem downloads.

This release could not have been possible without  Jim Piavis and his team at Microsoft.  We'd like to propose a formal vote of thanks  for their dedicated effort in getting this released.

# Version 1.0 release
We are launching the **Chemistry Add-in for Microsoft Word v. 1.0** on 1 February 2011 and are also pleased to announce that we have become part of the Outercurve Foundation in the research accelerators gallery. 
