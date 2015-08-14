1. Copy the contents of this folder to the root folder of the Default Web Site in the server (by default "c:\inetpub\wwwroot")
2. For better maintainability, it is better to copy this to another folder, say D:\RXPLWebApp or something similar and point the Default Web Site in IIS to this folder.
3. Make sure the Application Pool of the application in IIS is running under .NET Framework 4.0 in Integrated mode. If not create a new application pool with these settings and assign it to the application.


Application Settings
---------------------------------
1. Once the application is set-up properly, open the web.config file present in the root folder of the application
2. Find the setting with the key "ADServiceUrl". Replace the value with the proper URL of the Password reset service and save the file.
3. Once all these settings are made, browse to http://localhost (if it is hosted on port 80, otherwise mention the port as well)
4. If everything is right, you should get an HTTP 400 error, stating that User Id is required.
5. To access the password reset page, you may use any one of the following URL format
	a. http://machineip:port/?userid=someuserid
	b. http://machineip:port/home/index?userid=someuserid
	
	Replace "machineip" with the IP/Host of the server
	Replace "port" with the port on which the application is hosted (ignore this, if hosted on port 80)
	Replace "someuserid" with the user id of the user you wish to reset the password.