SELECT * FROM ga_hagevenner.members;

insert into members(UserName, FirstName, LastName, HashedPassword, Salt, Email, Created, Updated, IsAdminUser)

# yngve:hemmelig

values("Yngve", 
		"Yngve", 
        "Magnussen", 
        "$2a$11$B0X0zfKssgRHdM3E0Kdgwus3HwtpShhhHhxQoT5vG6cZkA2MCpaMW",
        "$2a$11$B0X0zfKssgRHdM3E0Kdgwu", 
        'yngve.magnussen@gokstadakademiet.no', 
        now(), 
        now(),
        true);

insert into members(UserName, FirstName, LastName, HashedPassword, Salt, Email, Created, Updated, IsAdminUser)

# kris:K1istoffer#

values("kris", 
		"Kristoffer", 
        "Sveberg", 
        "$2a$11$u1Q2UhBxVyGODkeXHuZQ0.1cbckoS2zdOF6.Q0Z8KAZtduQH3InVO",
        "$2a$11$u1Q2UhBxVyGODkeXHuZQ0.", 
        'kristoffer@gmail.com', 
        now(), 
        now(),
        false);
        
insert into members(UserName, FirstName, LastName, HashedPassword, Salt, Email, Created, Updated, IsAdminUser)

# anne:E1ger#

values("anne", 
		"Anne", 
        "Enger", 
        "$2a$11$jzYi76tmgv6Zgfe4Ec.Wv.k4LepqnhoDc/APDBBHp5/tM9uLnCL6W",
        "$2a$11$jzYi76tmgv6Zgfe4Ec.Wv.", 
        'anne@gmail.com', 
        now(), 
        now(),
        false);