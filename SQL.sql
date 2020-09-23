Use Education;

INSERT INTO AspNetRoles(Id, Name)VALUES 
('Admin', 'Admin'),
('TrainingStaff','TrainingStaff'),
('Trainer','Trainer'),
('Trainee','Trainee');

/*Username: admin@gmail.com Password: Admin@123*/

INSERT INTO dbo.AspNetUsers(Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName) VALUES 
('2a687d25-2737-4021-8f49-6898ea9af7ba','admin@gmail.com', 0,'ANkpLVn84qrt/b1wcHhqwoSvPjzOIu3bOSUodTb4WczUnlwX0OUErX+Rzn4KlMFqzQ==','b9b1d8d9-1f33-48f1-b3e1-4da825c70b4b',NULL,0,0,NULL,1,'0','admin@gmail.com');

INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
('2a687d25-2737-4021-8f49-6898ea9af7ba', 'Admin');