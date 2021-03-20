# ATFA
Automation Tools For (industrial) Automation

I hope in the end, this thing will become a incredible framework for PLC programing :)

The goal of this software is to create a set of tools to create PLC code automaticaly. Since our code needs to fallow a template, 
why not add parameters to the template to automaticaly generates the proper code for us?

This will reduce coding time, increase proper code formatting and finally we will be able to work together on the same project.
It also will force people to use the same code convention since they will not write the code anymore :thumbs_up:

This software will generates for you:
  - All the neccessary instances of FB's you need to control the machine, each FB's with its own or share constant parameter
  - An automatic mode with all the neccessary automatic process. Each element (only valves at the moment) is activated during a given list of steps
  - A list of collision avoidence. You can specify with element mouvement cannot be done depending on other element states
  - A manual mode that take account of the collision list avoidence. You can desactivate the list by setting (TODO give a name) a given global variable

This first version will just be able to manage a set of 2 positions cylinders. Enough to show the power of this tool.

It is good to know that all data are parsed using JSON format instead of a weired serialized format that most of the PLC IDE often use
