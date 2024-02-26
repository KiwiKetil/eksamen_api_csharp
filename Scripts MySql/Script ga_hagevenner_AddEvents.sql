SELECT * FROM ga_hagevenner.events;

insert into events(MemberId, EventType, EventName, Description, EventDate, EventTime, Created, Updated)

values(1,
"Plantemarked", 
		"Yngves plantemarked", 
        "En årlig begivenhet hvor medlemmer kan kjøpe, selge eller bytte planter, frø og hageutstyr.", 
		'2024-06-15',
        '10:00:00',
        now(),
        now()
        );
        
insert into events(MemberId, EventType, EventName, Description, EventDate, EventTime, Created, Updated)
        
        values(2,
"Hagearbeidsdag", 
		"Kristoffers superdugnad", 
        "En felles dugnad for å vedlikeholde og forbedre felles hageområder.", 
		'2024-07-15',
        '12:00:00',
        now(),
        now()
        );        
               
insert into events(MemberId, EventType, EventName, Description, EventDate, EventTime, Created, Updated)
        
        values(3,
"HageForedrag", 
		"Anne's HageForedrag", 
        "Foredrag og presentasjoner av eksperter på hagebruk, bærekraft og miljø.", 
		'2024-05-10',
        '09:00:00',
        now(),
        now()
        );
        
