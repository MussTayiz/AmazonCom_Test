# AmazonCom NUnit 3 & Selenium & Log4Net

- Open browser	 ✔
• Enter https://www.amazon.com/     ✔
• Create an account 	              ✔
• Login with created account  	    ✔
• Assert login successfully with created account 	✔
• Click Create a list (it is under Account & Lists tab) 	✔
• Create a public shopping list 	✔
• Assert shopping list is created successfully 	✔
• Add an idea into the shopping list (i.e Amazon Kindle) 	✔
• Assert added idea is in list • Add one more idea to the shopping list (i.e iPhone 8) 	✔
• Move this idea to Wish List • Assert this idea is moved to wish list successfully 	✔
• Delete added idea from wish list 	✔
• Assert idea is deleted from wish list successfully 	✔
• Return shopping list • Click Top Search Results button 	✔
• Go to third page • Assert 7th search result contains searched keyword 	✔
- Quit browser 	✔

transactions were performed.


#To make it work ;
Several changes must be made to the UnitTest1.cs file.
For the registration process;
string [] DataOfTheForm = {"TestName", "abc@gmail.com", "test123456 #"};
sequence must be edited.
To log in and all other tests;
string [] LoginData = {"abc@gmail.com", "test123456 #"};
sequence must be edited.

#Required packages;
- DotNetSeleniumExtras.PageObjects.Core
- log4net
- Microsoft.Net.Test.Sdk
- NUnit
- NUnit3TestAdapter
- Selenium.Chrome.Driver
- Selenium.WebDriver
