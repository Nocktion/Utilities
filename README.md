# Utilities
Hey there!
This repo contains a few handy utility scripts for C#.

<h2>List of scripts</h2>
<b>SystemInfo.cs</b><br>
Using the GetSystemInfo() method, you can get a nice little string containing all necessary information about the current system. Which looks like this in the Console:<br>
![System Info output](https://user-images.githubusercontent.com/27568659/142200208-6370ad84-5f68-469c-b445-31a953653671.png)
<br>
<br>
Note: SystemInfo requires a reference to Microfost.VisualBasic and System.Management to work.
<br>
<br>
<b>MSSQL.cs</b><br>
A little wrapper script for MSSQL, that simplifies the connection and querying in the database.<br>

The Constructor accepts a Server, a username, a password, and optionally a database name. The QueryRead(string query) returns an SqlDataReader for the given query, while the Query(string query) method, executes the query and returns the number of rows affected.<br>

The SqlConnection instance can be accessed as the conn variable. And the Close() method can be used to close the connection, however the connection is closed in the destructor eitherway.
