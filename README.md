# Task1-Systel__
 The features of this GUI is as follows :  
1- In the first form you have button to access each of the three forms (group devices, user, groups)  
2- Each of the 3 forms has a datagridview that displays a table that contains the data of its databse  
3- Each form has 4 button (Clear - Update - Insert - Delete)  
      Clear Button: To return all text boxes empty  
      Update Button: saves any changes you made in the table in the datagridview and if you closed without update you will be asked if you want to save  
      Insert Button: Inserts the data written in the text boxes in the database and updates the table with the new data  
      Delete Button: deletes the selected row and asks you if you are sure about deleting this row  
      Find in the users the selected groupid Button: this Button is int he groups table only it displays all the user having the groupid of the selected group  
      Some Errors are handled:  
                               If you tried to insert any primary key already exist there is a message box to inform you about that  
                               If any of the not null data is left empty during insertion you will also be informed about that  
                               If you tried to insert a groupid which deosn't exist in the groups you will also get informed  
                               If any wrong direct edit is done to the table a message box conataining a message "Soemthing wento wrong will appear"  

  All of the text boxes act as search.
