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

ALTER TABLE public.refreshtokens
    OWNER to postgres;

CREATE TABLE IF NOT EXISTS public.users
(
    userid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    username character varying(100) COLLATE pg_catalog."default" NOT NULL,
    emailaddress character varying(100) COLLATE pg_catalog."default" NOT NULL,
    password character varying(100) COLLATE pg_catalog."default" NOT NULL,
    expirydate date NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (userid)
);

ALTER TABLE public.users
    OWNER to postgres;

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

ALTER TABLE public.shoppinglists
    OWNER to postgres;

-- Table: public.shoppinglistitems

-- DROP TABLE public.shoppinglistitems;

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

ALTER TABLE public.shoppinglistitems
    OWNER to postgres;