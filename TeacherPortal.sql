CREATE TABLE User_Table (
    User_Id INTEGER PRIMARY KEY AUTOINCREMENT,
    User_Name TEXT UNIQUE,
    User_Password TEXT,
    User_Phone TEXT,
    User_CNIC TEXT UNIQUE,
    User_DOB TEXT,
    User_Gender TEXT,
    User_Email TEXT,
    User_Role TEXT,
    User_Address TEXT
);

SELECT * FROM User_Table

INSERT INTO User_Table 
(User_Name, User_Password, User_Phone, User_CNIC, User_DOB, User_Gender, User_Email, User_Role, User_Address) 
VALUES 
('admin', '123456', '+63 9498251349', '111111-111111-1', '03/21/2001', 'Male', 'admin@gmail.com', 'Admin', 'Philippines');
