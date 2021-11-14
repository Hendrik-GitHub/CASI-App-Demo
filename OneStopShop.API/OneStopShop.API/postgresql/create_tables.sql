CREATE TABLE IF NOT EXISTS public.refreshtokens
(
    tokenid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    userid integer NOT NULL,
    token character varying COLLATE pg_catalog."default" NOT NULL,
    expires timestamp without time zone NOT NULL,
    created timestamp without time zone NOT NULL,
    createdbyipaddress character varying(15) COLLATE pg_catalog."default" NOT NULL,
    revoked timestamp without time zone,
    revokedbyipaddress character varying(15) COLLATE pg_catalog."default",
    replacedbytoken character varying COLLATE pg_catalog."default",
    CONSTRAINT refreshtokens_pkey PRIMARY KEY (tokenid)
);

CREATE TABLE IF NOT EXISTS public.users
(
    userid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    username character varying(100) COLLATE pg_catalog."default" NOT NULL,
    emailaddress character varying(100) COLLATE pg_catalog."default" NOT NULL,
    password character varying(100) COLLATE pg_catalog."default" NOT NULL,
    expirydate date NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (userid)
);

CREATE TABLE IF NOT EXISTS public.shoppinglists
(
    shoppinglistid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    name character varying(150) COLLATE pg_catalog."default" NOT NULL,
    description character varying(500) COLLATE pg_catalog."default" NOT NULL,
    userid integer NOT NULL,
    insertiondate timestamp without time zone NOT NULL,
    CONSTRAINT shoppinglists_pkey PRIMARY KEY (shoppinglistid),
    CONSTRAINT fk_shoppinglists_users FOREIGN KEY (userid)
        REFERENCES public.users (userid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);


CREATE TABLE IF NOT EXISTS public.shoppinglistitems
(
    itemid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    shoppinglistitemdescription character varying(250) COLLATE pg_catalog."default" NOT NULL,
    shoppinglistid integer NOT NULL,
    quantitydescription character varying(250) COLLATE pg_catalog."default" NOT NULL,
    insertiondate timestamp without time zone NOT NULL,
    itemchecked boolean NOT NULL,
    CONSTRAINT shoppinglistitems_pkey PRIMARY KEY (itemid),
    CONSTRAINT fk_shoppinglistitems_shoppinglistid FOREIGN KEY (shoppinglistid)
        REFERENCES public.shoppinglists (shoppinglistid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);


CREATE TABLE IF NOT EXISTS public.category
(
    categoryid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    categoryname character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Category_pkey" PRIMARY KEY (categoryid)
);

CREATE TABLE IF NOT EXISTS public.log
(
    logid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    appdomainname character varying COLLATE pg_catalog."default" NOT NULL,
    eventid integer,
    machinename character varying COLLATE pg_catalog."default" NOT NULL,
    message character varying COLLATE pg_catalog."default",
    priority integer NOT NULL,
    processid character varying COLLATE pg_catalog."default" NOT NULL,
    processname character varying COLLATE pg_catalog."default" NOT NULL,
    severity character varying COLLATE pg_catalog."default" NOT NULL,
    threadname character varying COLLATE pg_catalog."default" NOT NULL,
    "timestamp" date NOT NULL,
    title character varying COLLATE pg_catalog."default" NOT NULL,
    win32threadid character varying COLLATE pg_catalog."default",
    formattedmessage character varying COLLATE pg_catalog."default",
    CONSTRAINT "Log_pkey" PRIMARY KEY (logid)
);

CREATE TABLE IF NOT EXISTS public.categorylog
(
    categorylogid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    categoryid integer NOT NULL,
    logid integer NOT NULL,
    CONSTRAINT "CategoryLog_pkey" PRIMARY KEY (categorylogid),
    CONSTRAINT fk_categorylog_category FOREIGN KEY (categoryid)
        REFERENCES public.category (categoryid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_categorylog_log FOREIGN KEY (logid)
        REFERENCES public.log (logid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE OR REPLACE PROCEDURE public.writelog(
	eventid integer,
	priority integer,
	severity character varying,
	title character varying,
	machinename character varying,
	appdomainname character varying,
	processid character varying,
	processname character varying,
	threadname character varying,
	win32threadid character varying,
	message character varying,
	formattedmessage character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN         
   INSERT INTO log (eventid,
		priority,
		severity,
		title,
		timestamp,
		machinename,
		appdomainname,
		processid,
		processname,
		threadname,
		win32threadid,
		message,
		formattedmessage) 
   VALUES   
    (eventid,
	priority,
	severity,
	title,
	NOW(),
	machinename,
	appdomainname,
	processid,
	processname,
	threadname,
	win32threadid,
	message,
	formattedmessage  
    ); 
END
$BODY$;
