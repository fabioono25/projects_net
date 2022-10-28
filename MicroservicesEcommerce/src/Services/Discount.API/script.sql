CREATE TABLE Coupon(
	 ID SERIAL PRIMARY KEY NOT NULL,
	 ProductName VARCHAR(24) NOT NULL,
	 Description TEXT,
	 Amount INT
 );
 
INSERT INTO Coupon(ProductName, Description, Amount) VALUES ('Iphone 14', 'IPhone 14 discount', 200);
 
INSERT INTO Coupon(ProductName, Description, Amount) VALUES ('Galaxy 20', 'Samsung Galaxy 20 discount', 100);