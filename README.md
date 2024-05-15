# BIOS

What is <b>BIOS</b>?
<hr>
BIOS is a web application that enables customers to browse components, compatible component elements, and make purchases of selected items.<br>
BIOS was made by: <br>
<ul>
  <li><a href="https://github.com/OmarColakhodzic">Omar Čolakhodžić</a></li>
  <li><a href="https://github.com/AdnanHumackic">Adnan Humačkić</a></li>
</ul>
<br>
<b>Functionalities</b>
<hr>
<b>Customer</b>
Customer has the option to view items, filter, perform detailed inspection, review compatible item elements, and add selected items to the wishlist, cart, and proceed with the purchase.
<br>
<b>Admin</b>
Admin possesses their own admin panel, where they can add employees, manage user profiles, assign tasks to employees, view active tasks, and admins have the capability to upload documents for employees.

<b>Employee</b>
The primary focus of the employee is working with components, including adding, deleting, and editing components. Additionally, they have the ability to view tasks assigned to them by the admin, review active orders, and access documents published by the admin.<br>


<b>All of the mentioned entities also have the capability to edit their user profile.</b><br>
<hr>
<b>Component browsing</b>
<br>
<img src="https://github.com/AdnanHumackic/BIOS/assets/117025277/41db0a56-db0e-4040-98d5-c931834d2b26"/>

<b>Admin panel</b>
<br>
![image](https://github.com/AdnanHumackic/BIOS/assets/117025277/2ea9e6f4-ba2b-4f31-9a14-7d9eb45c0d65)

<b>Employee panel</b>
<br>
![image](https://github.com/AdnanHumackic/BIOS/assets/117025277/fdcf8e21-d54b-4011-903a-cff1d11ac5a8)

<hr>

<b>Instructions for use</b><br>
<ul>
  <li>Start PCShop_api.sln from PCShop_api directory</li>
  <li>Inside package manager console insert command: update-database</li>
  <li>Start the project</li>
  <li>Call the controller /Podaci/Generisi using Swagger</li>
  <li>Go to the folder Frontend and execute the command "npm install"</li>
  <li>Start the application with command "ng serve"</li>
  <li>Login data for customer: username: kupac password: test</li>
  <li>Login data for admin: username: admin password: test</li>
  <li>Login data for employee: username: radnik password: test</li>
  <li>Application is ready for use</li>
</ul>


<b>The 'db_backup' folder contains database backup that can be used for testing the application, to avoid re-adding items</b>




