PGDMP  (                	    |            Clowns    16.4    16.4 �    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16397    Clowns    DATABASE        CREATE DATABASE "Clowns" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_Australia.1252';
    DROP DATABASE "Clowns";
                postgres    false            �            1259    32781    Addons    TABLE     �   CREATE TABLE public."Addons" (
    "AddonId" integer NOT NULL,
    "AddonName" character varying(100) NOT NULL,
    "Price" double precision NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."Addons";
       public         heap    postgres    false            �            1259    32780    Addons_AddonId_seq    SEQUENCE     �   ALTER TABLE public."Addons" ALTER COLUMN "AddonId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Addons_AddonId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    242            �            1259    24577    AddressType    TABLE     �   CREATE TABLE public."AddressType" (
    "AddressTypeId" integer NOT NULL,
    "AddressTypeName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 !   DROP TABLE public."AddressType";
       public         heap    postgres    false            �            1259    24576    AddressType_AddressTypeId_seq    SEQUENCE     �   ALTER TABLE public."AddressType" ALTER COLUMN "AddressTypeId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."AddressType_AddressTypeId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    224            �            1259    32787    Bounces    TABLE     �   CREATE TABLE public."Bounces" (
    "BounceId" integer NOT NULL,
    "BounceName" character varying(100) NOT NULL,
    "Price" double precision NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."Bounces";
       public         heap    postgres    false            �            1259    32786    Bounces_BounceId_seq    SEQUENCE     �   ALTER TABLE public."Bounces" ALTER COLUMN "BounceId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Bounces_BounceId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    244            �            1259    32833    CardOptions    TABLE     �   CREATE TABLE public."CardOptions" (
    "CardOptionId" integer NOT NULL,
    "CardOptionName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 !   DROP TABLE public."CardOptions";
       public         heap    postgres    false            �            1259    32832    CardOptions_CardOptionId_seq    SEQUENCE     �   ALTER TABLE public."CardOptions" ALTER COLUMN "CardOptionId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CardOptions_CardOptionId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    254            �            1259    32793    Category    TABLE     �   CREATE TABLE public."Category" (
    "CategoryId" integer NOT NULL,
    "CategoryName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."Category";
       public         heap    postgres    false            �            1259    32792    Category_CategoryId_seq    SEQUENCE     �   ALTER TABLE public."Category" ALTER COLUMN "CategoryId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Category_CategoryId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    246            �            1259    32775 
   Characters    TABLE     �   CREATE TABLE public."Characters" (
    "CharacterId" integer NOT NULL,
    "CharacterName" character varying(100) NOT NULL,
    "Price" double precision NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
     DROP TABLE public."Characters";
       public         heap    postgres    false            �            1259    32774    Characters_CharacterId_seq    SEQUENCE     �   ALTER TABLE public."Characters" ALTER COLUMN "CharacterId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Characters_CharacterId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    240            �            1259    24583    Children    TABLE     �   CREATE TABLE public."Children" (
    "ChildrenId" integer NOT NULL,
    "ChildrenNo" integer NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."Children";
       public         heap    postgres    false            �            1259    24589    ChildrenUnderAge    TABLE     �   CREATE TABLE public."ChildrenUnderAge" (
    "ChildrenUnderAgeId" integer NOT NULL,
    "ChildrenUnderAgeNo" integer NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 &   DROP TABLE public."ChildrenUnderAge";
       public         heap    postgres    false            �            1259    24588 '   ChildrenUnderAge_ChildrenUnderAgeId_seq    SEQUENCE       ALTER TABLE public."ChildrenUnderAge" ALTER COLUMN "ChildrenUnderAgeId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."ChildrenUnderAge_ChildrenUnderAgeId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    228            �            1259    24582    Children_ChildrenId_seq    SEQUENCE     �   ALTER TABLE public."Children" ALTER COLUMN "ChildrenId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Children_ChildrenId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    226                       1259    41011    ContractStatus    TABLE     �   CREATE TABLE public."ContractStatus" (
    "ContractStatusId" integer NOT NULL,
    "ContractStatusName" character varying(200),
    "ContractStatusColor" character varying(100)
);
 $   DROP TABLE public."ContractStatus";
       public         heap    postgres    false                       1259    41010 #   ContractStatus_ContractStatusId_seq    SEQUENCE     �   ALTER TABLE public."ContractStatus" ALTER COLUMN "ContractStatusId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."ContractStatus_ContractStatusId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    268            
           1259    33082    Contract_BookingPaymentInfo    TABLE     9  CREATE TABLE public."Contract_BookingPaymentInfo" (
    "BookingPaymentInfoId" integer NOT NULL,
    "CustomerId" integer,
    "ContractId" integer,
    "BranchId" integer,
    "CompanyId" integer,
    "CardTypeId" integer,
    "ExpireMonthYear" character varying(10),
    "CVV" integer,
    "CardTypeId2" integer,
    "ExpireMonthYear2" character varying(10),
    "CVV2" integer,
    "PaymentStatusId" integer,
    "BillingAddress" character varying(1000),
    "UseAddress" boolean,
    "CardNumber" character varying(100),
    "CardNumber2" character varying(100)
);
 1   DROP TABLE public."Contract_BookingPaymentInfo";
       public         heap    postgres    false            	           1259    33081 4   Contract_BookingPaymentInfo_BookingPaymentInfoId_seq    SEQUENCE       ALTER TABLE public."Contract_BookingPaymentInfo" ALTER COLUMN "BookingPaymentInfoId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_BookingPaymentInfo_BookingPaymentInfoId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    266            �            1259    32819    Contract_EventInfo    TABLE     1  CREATE TABLE public."Contract_EventInfo" (
    "Contract_EventInfoId" integer NOT NULL,
    "EventInfo_EventType" integer,
    "EventInfo_NumberOfChildren" integer,
    "EventInfo_EventDate" date,
    "EventInfo_TeamAssigned" integer,
    "EventInfo_EventAddress" character varying(1000),
    "EventInfo_EventCity" character varying(100),
    "EventInfo_EventZip" integer,
    "EventInfo_EventState" integer,
    "EventInfo_Venue" integer,
    "EventInfo_VenueDescription" character varying(1000),
    "ContractId" integer,
    "CustomerId" integer,
    "EventInfo_PartyStartTime" time without time zone,
    "EventInfo_PartyEndTime" time without time zone,
    "EventInfo_StartClownHour" time without time zone,
    "EventInfo_EndClownHour" time without time zone,
    "BranchId" integer,
    "CompanyId" integer
);
 (   DROP TABLE public."Contract_EventInfo";
       public         heap    postgres    false            �            1259    32818 +   Contract_EventInfo_Contract_EventInfoId_seq    SEQUENCE     	  ALTER TABLE public."Contract_EventInfo" ALTER COLUMN "Contract_EventInfoId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_EventInfo_Contract_EventInfoId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    252                       1259    32846    Contract_PackageInfo    TABLE        CREATE TABLE public."Contract_PackageInfo" (
    "PackageInfoId" integer NOT NULL,
    "CategoryId" integer,
    "PartyPackageId" integer,
    "Price" numeric(18,2),
    "Tax" numeric(18,2),
    "Tip" numeric(18,2),
    "Description" character varying(1000),
    "ParkingFees" numeric(18,2),
    "TollFees" numeric(18,2),
    "Deposit" numeric(18,2),
    "Tip2" numeric(18,2),
    "Subtract" numeric(18,2),
    "TotalBalance" numeric(18,2),
    "ContractId" integer,
    "CustomerId" integer,
    "BranchId" integer,
    "CompanyId" integer
);
 *   DROP TABLE public."Contract_PackageInfo";
       public         heap    postgres    false                       1259    32867    Contract_PackageInfo_Addon    TABLE       CREATE TABLE public."Contract_PackageInfo_Addon" (
    "Contract_PackageInfo_AddonId" integer NOT NULL,
    "PackageInfoId" integer NOT NULL,
    "AddonId" integer NOT NULL,
    "Price" numeric(18,2) NOT NULL,
    "BranchId" integer NOT NULL,
    "CompanyId" integer NOT NULL
);
 0   DROP TABLE public."Contract_PackageInfo_Addon";
       public         heap    postgres    false                       1259    32866 ;   Contract_PackageInfo_Addon_Contract_PackageInfo_AddonId_seq    SEQUENCE     )  ALTER TABLE public."Contract_PackageInfo_Addon" ALTER COLUMN "Contract_PackageInfo_AddonId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_PackageInfo_Addon_Contract_PackageInfo_AddonId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    262                       1259    32878    Contract_PackageInfo_Bounce    TABLE       CREATE TABLE public."Contract_PackageInfo_Bounce" (
    "Contract_PackageInfo_BounceId" integer NOT NULL,
    "PackageInfoId" integer NOT NULL,
    "BounceId" integer NOT NULL,
    "Price" numeric(18,2) NOT NULL,
    "BranchId" integer NOT NULL,
    "CompanyId" integer NOT NULL
);
 1   DROP TABLE public."Contract_PackageInfo_Bounce";
       public         heap    postgres    false                       1259    32877 =   Contract_PackageInfo_Bounce_Contract_PackageInfo_BounceId_seq    SEQUENCE     -  ALTER TABLE public."Contract_PackageInfo_Bounce" ALTER COLUMN "Contract_PackageInfo_BounceId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_PackageInfo_Bounce_Contract_PackageInfo_BounceId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    264                       1259    32854    Contract_PackageInfo_Character    TABLE     �   CREATE TABLE public."Contract_PackageInfo_Character" (
    "Contract_PackageInfo_CharacterId" integer NOT NULL,
    "PackageInfoId" integer NOT NULL,
    "Price" numeric(18,2),
    "BranchId" integer,
    "CompanyId" integer,
    "CharacterId" integer
);
 4   DROP TABLE public."Contract_PackageInfo_Character";
       public         heap    postgres    false                       1259    32853 ?   Contract_PackageInfo_Characte_Contract_PackageInfo_Characte_seq    SEQUENCE     5  ALTER TABLE public."Contract_PackageInfo_Character" ALTER COLUMN "Contract_PackageInfo_CharacterId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_PackageInfo_Characte_Contract_PackageInfo_Characte_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    260                       1259    32845 &   Contract_PackageInfo_PackageInfoId_seq    SEQUENCE     �   ALTER TABLE public."Contract_PackageInfo" ALTER COLUMN "PackageInfoId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_PackageInfo_PackageInfoId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    258            �            1259    16424    Contract_TimeTeamInfo    TABLE       CREATE TABLE public."Contract_TimeTeamInfo" (
    "Contract_TimeTeamInfoId" bigint NOT NULL,
    "TeamId" integer NOT NULL,
    "TimeSlotId" integer NOT NULL,
    "ContractId" integer,
    "Date" date NOT NULL,
    "CustomerId" integer,
    "EntryDate" date,
    "BranchId" integer,
    "CompanyId" integer,
    "ContractNo" character varying(200),
    "ContractStatusId" integer
);
 +   DROP TABLE public."Contract_TimeTeamInfo";
       public         heap    postgres    false            �            1259    16423 1   Contract_TimeTeamInfo_Contract_TimeTeamInfoId_seq    SEQUENCE       ALTER TABLE public."Contract_TimeTeamInfo" ALTER COLUMN "Contract_TimeTeamInfoId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Contract_TimeTeamInfo_Contract_TimeTeamInfoId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �            1259    32811    CustomerInfo    TABLE       CREATE TABLE public."CustomerInfo" (
    "CustomerId" integer NOT NULL,
    "FirstName" character varying(300) NOT NULL,
    "LastName" character varying(200),
    "EmailAddress" character varying(100),
    "PhoneNo" character varying(100),
    "RelationshipId" integer,
    "OtherRelationshipId" integer,
    "AlternatePhone" character varying(100),
    "Address" character varying(500),
    "AddressTypeId" integer,
    "City" character varying(100),
    "Zip" integer,
    "StateId" integer,
    "ChildrenId" integer,
    "ChildrenUnderAgeId" integer,
    "HonoreeName" character varying(300),
    "HonoreeAge" integer,
    "HeardResourceId" integer,
    "Comments" character varying(1000),
    "specifyOther" character varying(100),
    "BranchId" integer,
    "CompanyId" integer
);
 "   DROP TABLE public."CustomerInfo";
       public         heap    postgres    false            �            1259    32810    CustomerInfo_CustomerId_seq    SEQUENCE     �   ALTER TABLE public."CustomerInfo" ALTER COLUMN "CustomerId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CustomerInfo_CustomerId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    250            �            1259    32769 	   EventType    TABLE     �   CREATE TABLE public."EventType" (
    "EventTypeId" integer NOT NULL,
    "EventTypeName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."EventType";
       public         heap    postgres    false            �            1259    32768    EventType_EventTypeId_seq    SEQUENCE     �   ALTER TABLE public."EventType" ALTER COLUMN "EventTypeId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."EventType_EventTypeId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    238            �            1259    24595    HeardResource    TABLE     �   CREATE TABLE public."HeardResource" (
    "HeardResourceId" integer NOT NULL,
    "HeardResourceName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 #   DROP TABLE public."HeardResource";
       public         heap    postgres    false            �            1259    24594 !   HeardResource_HeardResourceId_seq    SEQUENCE     �   ALTER TABLE public."HeardResource" ALTER COLUMN "HeardResourceId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."HeardResource_HeardResourceId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    230            �            1259    32801    PartyPackage    TABLE     �   CREATE TABLE public."PartyPackage" (
    "PartyPackageId" integer NOT NULL,
    "PartyPackageName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 "   DROP TABLE public."PartyPackage";
       public         heap    postgres    false            �            1259    32800    PartyPackage_PartyPackageId_seq    SEQUENCE     �   ALTER TABLE public."PartyPackage" ALTER COLUMN "PartyPackageId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."PartyPackage_PartyPackageId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    248                        1259    32839    PaymentStatus    TABLE     �   CREATE TABLE public."PaymentStatus" (
    "PaymentStatusId" integer NOT NULL,
    "PaymentStatusName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 #   DROP TABLE public."PaymentStatus";
       public         heap    postgres    false            �            1259    32838 !   PaymentStatus_PaymentStatusId_seq    SEQUENCE     �   ALTER TABLE public."PaymentStatus" ALTER COLUMN "PaymentStatusId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."PaymentStatus_PaymentStatusId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    256            �            1259    24601    Relationship    TABLE     �   CREATE TABLE public."Relationship" (
    "RelationshipId" integer NOT NULL,
    "RelationshipName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
 "   DROP TABLE public."Relationship";
       public         heap    postgres    false            �            1259    24600    Relationship_RelationshipId_seq    SEQUENCE     �   ALTER TABLE public."Relationship" ALTER COLUMN "RelationshipId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Relationship_RelationshipId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    232            �            1259    24607    State    TABLE     �   CREATE TABLE public."State" (
    "StateId" integer NOT NULL,
    "StateName" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."State";
       public         heap    postgres    false            �            1259    24606    State_StateId_seq    SEQUENCE     �   ALTER TABLE public."State" ALTER COLUMN "StateId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."State_StateId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    234            �            1259    16411    Teams    TABLE     �   CREATE TABLE public."Teams" (
    "TeamId" integer NOT NULL,
    "TeamNo" character varying NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."Teams";
       public         heap    postgres    false            �            1259    16410    Teams_TeamId_seq    SEQUENCE     �   ALTER TABLE public."Teams" ALTER COLUMN "TeamId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Teams_TeamId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            �            1259    16405    TimeSlot    TABLE     �   CREATE TABLE public."TimeSlot" (
    "TimeSlotId" integer NOT NULL,
    "Time" character varying(100) NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."TimeSlot";
       public         heap    postgres    false            �            1259    16404    TimeSlot_TimeSlotId_seq    SEQUENCE     �   ALTER TABLE public."TimeSlot" ALTER COLUMN "TimeSlotId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."TimeSlot_TimeSlotId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    218            �            1259    24612    Venue    TABLE     �   CREATE TABLE public."Venue" (
    "VenueId" integer NOT NULL,
    "VenueName" character varying NOT NULL,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."Venue";
       public         heap    postgres    false            �            1259    24615    Venue_VenueId_seq    SEQUENCE     �   ALTER TABLE public."Venue" ALTER COLUMN "VenueId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Venue_VenueId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    235            �            1259    16399    user    TABLE       CREATE TABLE public."user" (
    "UserId" integer NOT NULL,
    "Username" character varying(100) NOT NULL,
    "EmailAddress" character varying(100) NOT NULL,
    "Password" character varying(100) NOT NULL,
    "IsActive" boolean,
    "BranchId" integer,
    "CompanyId" integer
);
    DROP TABLE public."user";
       public         heap    postgres    false            �            1259    16398    user_UserId_seq    SEQUENCE     �   ALTER TABLE public."user" ALTER COLUMN "UserId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."user_UserId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216            �          0    32781    Addons 
   TABLE DATA           \   COPY public."Addons" ("AddonId", "AddonName", "Price", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    242   ��       �          0    24577    AddressType 
   TABLE DATA           d   COPY public."AddressType" ("AddressTypeId", "AddressTypeName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    224   ��       �          0    32787    Bounces 
   TABLE DATA           _   COPY public."Bounces" ("BounceId", "BounceName", "Price", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    244   ��       �          0    32833    CardOptions 
   TABLE DATA           b   COPY public."CardOptions" ("CardOptionId", "CardOptionName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    254   !�       �          0    32793    Category 
   TABLE DATA           [   COPY public."Category" ("CategoryId", "CategoryName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    246   R�       �          0    32775 
   Characters 
   TABLE DATA           h   COPY public."Characters" ("CharacterId", "CharacterName", "Price", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    240   ��       �          0    24583    Children 
   TABLE DATA           Y   COPY public."Children" ("ChildrenId", "ChildrenNo", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    226   ��       �          0    24589    ChildrenUnderAge 
   TABLE DATA           q   COPY public."ChildrenUnderAge" ("ChildrenUnderAgeId", "ChildrenUnderAgeNo", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    228   ��       �          0    41011    ContractStatus 
   TABLE DATA           k   COPY public."ContractStatus" ("ContractStatusId", "ContractStatusName", "ContractStatusColor") FROM stdin;
    public          postgres    false    268   �       �          0    33082    Contract_BookingPaymentInfo 
   TABLE DATA           '  COPY public."Contract_BookingPaymentInfo" ("BookingPaymentInfoId", "CustomerId", "ContractId", "BranchId", "CompanyId", "CardTypeId", "ExpireMonthYear", "CVV", "CardTypeId2", "ExpireMonthYear2", "CVV2", "PaymentStatusId", "BillingAddress", "UseAddress", "CardNumber", "CardNumber2") FROM stdin;
    public          postgres    false    266   w�       �          0    32819    Contract_EventInfo 
   TABLE DATA           �  COPY public."Contract_EventInfo" ("Contract_EventInfoId", "EventInfo_EventType", "EventInfo_NumberOfChildren", "EventInfo_EventDate", "EventInfo_TeamAssigned", "EventInfo_EventAddress", "EventInfo_EventCity", "EventInfo_EventZip", "EventInfo_EventState", "EventInfo_Venue", "EventInfo_VenueDescription", "ContractId", "CustomerId", "EventInfo_PartyStartTime", "EventInfo_PartyEndTime", "EventInfo_StartClownHour", "EventInfo_EndClownHour", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    252   �       �          0    32846    Contract_PackageInfo 
   TABLE DATA             COPY public."Contract_PackageInfo" ("PackageInfoId", "CategoryId", "PartyPackageId", "Price", "Tax", "Tip", "Description", "ParkingFees", "TollFees", "Deposit", "Tip2", "Subtract", "TotalBalance", "ContractId", "CustomerId", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    258   ��       �          0    32867    Contract_PackageInfo_Addon 
   TABLE DATA           �   COPY public."Contract_PackageInfo_Addon" ("Contract_PackageInfo_AddonId", "PackageInfoId", "AddonId", "Price", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    262   o�       �          0    32878    Contract_PackageInfo_Bounce 
   TABLE DATA           �   COPY public."Contract_PackageInfo_Bounce" ("Contract_PackageInfo_BounceId", "PackageInfoId", "BounceId", "Price", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    264   `�       �          0    32854    Contract_PackageInfo_Character 
   TABLE DATA           �   COPY public."Contract_PackageInfo_Character" ("Contract_PackageInfo_CharacterId", "PackageInfoId", "Price", "BranchId", "CompanyId", "CharacterId") FROM stdin;
    public          postgres    false    260   S�       �          0    16424    Contract_TimeTeamInfo 
   TABLE DATA           �   COPY public."Contract_TimeTeamInfo" ("Contract_TimeTeamInfoId", "TeamId", "TimeSlotId", "ContractId", "Date", "CustomerId", "EntryDate", "BranchId", "CompanyId", "ContractNo", "ContractStatusId") FROM stdin;
    public          postgres    false    222   G�       �          0    32811    CustomerInfo 
   TABLE DATA           d  COPY public."CustomerInfo" ("CustomerId", "FirstName", "LastName", "EmailAddress", "PhoneNo", "RelationshipId", "OtherRelationshipId", "AlternatePhone", "Address", "AddressTypeId", "City", "Zip", "StateId", "ChildrenId", "ChildrenUnderAgeId", "HonoreeName", "HonoreeAge", "HeardResourceId", "Comments", "specifyOther", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    250   ��       �          0    32769 	   EventType 
   TABLE DATA           ^   COPY public."EventType" ("EventTypeId", "EventTypeName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    238   y�       �          0    24595    HeardResource 
   TABLE DATA           j   COPY public."HeardResource" ("HeardResourceId", "HeardResourceName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    230   ��       �          0    32801    PartyPackage 
   TABLE DATA           g   COPY public."PartyPackage" ("PartyPackageId", "PartyPackageName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    248   ��       �          0    32839    PaymentStatus 
   TABLE DATA           j   COPY public."PaymentStatus" ("PaymentStatusId", "PaymentStatusName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    256   �       �          0    24601    Relationship 
   TABLE DATA           g   COPY public."Relationship" ("RelationshipId", "RelationshipName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    232   U�       �          0    24607    State 
   TABLE DATA           R   COPY public."State" ("StateId", "StateName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    234   ��       �          0    16411    Teams 
   TABLE DATA           N   COPY public."Teams" ("TeamId", "TeamNo", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    220   ��       �          0    16405    TimeSlot 
   TABLE DATA           S   COPY public."TimeSlot" ("TimeSlotId", "Time", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    218   6�       �          0    24612    Venue 
   TABLE DATA           R   COPY public."Venue" ("VenueId", "VenueName", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    235   �       �          0    16399    user 
   TABLE DATA           w   COPY public."user" ("UserId", "Username", "EmailAddress", "Password", "IsActive", "BranchId", "CompanyId") FROM stdin;
    public          postgres    false    216   A�       �           0    0    Addons_AddonId_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."Addons_AddonId_seq"', 2, true);
          public          postgres    false    241            �           0    0    AddressType_AddressTypeId_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public."AddressType_AddressTypeId_seq"', 2, true);
          public          postgres    false    223            �           0    0    Bounces_BounceId_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Bounces_BounceId_seq"', 2, true);
          public          postgres    false    243            �           0    0    CardOptions_CardOptionId_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public."CardOptions_CardOptionId_seq"', 2, true);
          public          postgres    false    253            �           0    0    Category_CategoryId_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public."Category_CategoryId_seq"', 2, true);
          public          postgres    false    245            �           0    0    Characters_CharacterId_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Characters_CharacterId_seq"', 2, true);
          public          postgres    false    239            �           0    0 '   ChildrenUnderAge_ChildrenUnderAgeId_seq    SEQUENCE SET     W   SELECT pg_catalog.setval('public."ChildrenUnderAge_ChildrenUnderAgeId_seq"', 1, true);
          public          postgres    false    227            �           0    0    Children_ChildrenId_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public."Children_ChildrenId_seq"', 3, true);
          public          postgres    false    225            �           0    0 #   ContractStatus_ContractStatusId_seq    SEQUENCE SET     S   SELECT pg_catalog.setval('public."ContractStatus_ContractStatusId_seq"', 5, true);
          public          postgres    false    267            �           0    0 4   Contract_BookingPaymentInfo_BookingPaymentInfoId_seq    SEQUENCE SET     e   SELECT pg_catalog.setval('public."Contract_BookingPaymentInfo_BookingPaymentInfoId_seq"', 25, true);
          public          postgres    false    265            �           0    0 +   Contract_EventInfo_Contract_EventInfoId_seq    SEQUENCE SET     ]   SELECT pg_catalog.setval('public."Contract_EventInfo_Contract_EventInfoId_seq"', 171, true);
          public          postgres    false    251            �           0    0 ;   Contract_PackageInfo_Addon_Contract_PackageInfo_AddonId_seq    SEQUENCE SET     m   SELECT pg_catalog.setval('public."Contract_PackageInfo_Addon_Contract_PackageInfo_AddonId_seq"', 175, true);
          public          postgres    false    261            �           0    0 =   Contract_PackageInfo_Bounce_Contract_PackageInfo_BounceId_seq    SEQUENCE SET     o   SELECT pg_catalog.setval('public."Contract_PackageInfo_Bounce_Contract_PackageInfo_BounceId_seq"', 175, true);
          public          postgres    false    263            �           0    0 ?   Contract_PackageInfo_Characte_Contract_PackageInfo_Characte_seq    SEQUENCE SET     q   SELECT pg_catalog.setval('public."Contract_PackageInfo_Characte_Contract_PackageInfo_Characte_seq"', 180, true);
          public          postgres    false    259            �           0    0 &   Contract_PackageInfo_PackageInfoId_seq    SEQUENCE SET     W   SELECT pg_catalog.setval('public."Contract_PackageInfo_PackageInfoId_seq"', 81, true);
          public          postgres    false    257            �           0    0 1   Contract_TimeTeamInfo_Contract_TimeTeamInfoId_seq    SEQUENCE SET     c   SELECT pg_catalog.setval('public."Contract_TimeTeamInfo_Contract_TimeTeamInfoId_seq"', 253, true);
          public          postgres    false    221            �           0    0    CustomerInfo_CustomerId_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public."CustomerInfo_CustomerId_seq"', 195, true);
          public          postgres    false    249            �           0    0    EventType_EventTypeId_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public."EventType_EventTypeId_seq"', 2, true);
          public          postgres    false    237            �           0    0 !   HeardResource_HeardResourceId_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public."HeardResource_HeardResourceId_seq"', 2, true);
          public          postgres    false    229            �           0    0    PartyPackage_PartyPackageId_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public."PartyPackage_PartyPackageId_seq"', 2, true);
          public          postgres    false    247            �           0    0 !   PaymentStatus_PaymentStatusId_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public."PaymentStatus_PaymentStatusId_seq"', 3, true);
          public          postgres    false    255            �           0    0    Relationship_RelationshipId_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public."Relationship_RelationshipId_seq"', 2, true);
          public          postgres    false    231            �           0    0    State_StateId_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."State_StateId_seq"', 2, true);
          public          postgres    false    233            �           0    0    Teams_TeamId_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Teams_TeamId_seq"', 20, true);
          public          postgres    false    219            �           0    0    TimeSlot_TimeSlotId_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."TimeSlot_TimeSlotId_seq"', 48, true);
          public          postgres    false    217            �           0    0    Venue_VenueId_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Venue_VenueId_seq"', 2, true);
          public          postgres    false    236            �           0    0    user_UserId_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public."user_UserId_seq"', 1, true);
          public          postgres    false    215            �           2606    32785    Addons Addons_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public."Addons"
    ADD CONSTRAINT "Addons_pkey" PRIMARY KEY ("AddonId");
 @   ALTER TABLE ONLY public."Addons" DROP CONSTRAINT "Addons_pkey";
       public            postgres    false    242            �           2606    24581    AddressType AddressType_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public."AddressType"
    ADD CONSTRAINT "AddressType_pkey" PRIMARY KEY ("AddressTypeId");
 J   ALTER TABLE ONLY public."AddressType" DROP CONSTRAINT "AddressType_pkey";
       public            postgres    false    224                       2606    33086 3   Contract_BookingPaymentInfo BookingPaymentInfo_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Contract_BookingPaymentInfo"
    ADD CONSTRAINT "BookingPaymentInfo_pkey" PRIMARY KEY ("BookingPaymentInfoId");
 a   ALTER TABLE ONLY public."Contract_BookingPaymentInfo" DROP CONSTRAINT "BookingPaymentInfo_pkey";
       public            postgres    false    266            �           2606    32791    Bounces Bounces_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public."Bounces"
    ADD CONSTRAINT "Bounces_pkey" PRIMARY KEY ("BounceId");
 B   ALTER TABLE ONLY public."Bounces" DROP CONSTRAINT "Bounces_pkey";
       public            postgres    false    244            �           2606    32837    CardOptions CardOptions_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public."CardOptions"
    ADD CONSTRAINT "CardOptions_pkey" PRIMARY KEY ("CardOptionId");
 J   ALTER TABLE ONLY public."CardOptions" DROP CONSTRAINT "CardOptions_pkey";
       public            postgres    false    254            �           2606    32799    Category Category_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Category"
    ADD CONSTRAINT "Category_pkey" PRIMARY KEY ("CategoryId");
 D   ALTER TABLE ONLY public."Category" DROP CONSTRAINT "Category_pkey";
       public            postgres    false    246            �           2606    32779    Characters Characters_pkey 
   CONSTRAINT     g   ALTER TABLE ONLY public."Characters"
    ADD CONSTRAINT "Characters_pkey" PRIMARY KEY ("CharacterId");
 H   ALTER TABLE ONLY public."Characters" DROP CONSTRAINT "Characters_pkey";
       public            postgres    false    240            �           2606    24593 &   ChildrenUnderAge ChildrenUnderAge_pkey 
   CONSTRAINT     z   ALTER TABLE ONLY public."ChildrenUnderAge"
    ADD CONSTRAINT "ChildrenUnderAge_pkey" PRIMARY KEY ("ChildrenUnderAgeId");
 T   ALTER TABLE ONLY public."ChildrenUnderAge" DROP CONSTRAINT "ChildrenUnderAge_pkey";
       public            postgres    false    228            �           2606    24587    Children Children_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Children"
    ADD CONSTRAINT "Children_pkey" PRIMARY KEY ("ChildrenId");
 D   ALTER TABLE ONLY public."Children" DROP CONSTRAINT "Children_pkey";
       public            postgres    false    226                       2606    41015 "   ContractStatus ContractStatus_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public."ContractStatus"
    ADD CONSTRAINT "ContractStatus_pkey" PRIMARY KEY ("ContractStatusId");
 P   ALTER TABLE ONLY public."ContractStatus" DROP CONSTRAINT "ContractStatus_pkey";
       public            postgres    false    268            �           2606    32825 *   Contract_EventInfo Contract_EventInfo_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Contract_EventInfo"
    ADD CONSTRAINT "Contract_EventInfo_pkey" PRIMARY KEY ("Contract_EventInfoId");
 X   ALTER TABLE ONLY public."Contract_EventInfo" DROP CONSTRAINT "Contract_EventInfo_pkey";
       public            postgres    false    252                       2606    32871 :   Contract_PackageInfo_Addon Contract_PackageInfo_Addon_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Contract_PackageInfo_Addon"
    ADD CONSTRAINT "Contract_PackageInfo_Addon_pkey" PRIMARY KEY ("Contract_PackageInfo_AddonId");
 h   ALTER TABLE ONLY public."Contract_PackageInfo_Addon" DROP CONSTRAINT "Contract_PackageInfo_Addon_pkey";
       public            postgres    false    262                       2606    32882 <   Contract_PackageInfo_Bounce Contract_PackageInfo_Bounce_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Contract_PackageInfo_Bounce"
    ADD CONSTRAINT "Contract_PackageInfo_Bounce_pkey" PRIMARY KEY ("Contract_PackageInfo_BounceId");
 j   ALTER TABLE ONLY public."Contract_PackageInfo_Bounce" DROP CONSTRAINT "Contract_PackageInfo_Bounce_pkey";
       public            postgres    false    264            �           2606    32860 B   Contract_PackageInfo_Character Contract_PackageInfo_Character_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Contract_PackageInfo_Character"
    ADD CONSTRAINT "Contract_PackageInfo_Character_pkey" PRIMARY KEY ("Contract_PackageInfo_CharacterId");
 p   ALTER TABLE ONLY public."Contract_PackageInfo_Character" DROP CONSTRAINT "Contract_PackageInfo_Character_pkey";
       public            postgres    false    260            �           2606    32850 .   Contract_PackageInfo Contract_PackageInfo_pkey 
   CONSTRAINT     }   ALTER TABLE ONLY public."Contract_PackageInfo"
    ADD CONSTRAINT "Contract_PackageInfo_pkey" PRIMARY KEY ("PackageInfoId");
 \   ALTER TABLE ONLY public."Contract_PackageInfo" DROP CONSTRAINT "Contract_PackageInfo_pkey";
       public            postgres    false    258            �           2606    16428 0   Contract_TimeTeamInfo Contract_TimeTeamInfo_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."Contract_TimeTeamInfo"
    ADD CONSTRAINT "Contract_TimeTeamInfo_pkey" PRIMARY KEY ("Contract_TimeTeamInfoId");
 ^   ALTER TABLE ONLY public."Contract_TimeTeamInfo" DROP CONSTRAINT "Contract_TimeTeamInfo_pkey";
       public            postgres    false    222            �           2606    32817    CustomerInfo CustomerInfo_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public."CustomerInfo"
    ADD CONSTRAINT "CustomerInfo_pkey" PRIMARY KEY ("CustomerId");
 L   ALTER TABLE ONLY public."CustomerInfo" DROP CONSTRAINT "CustomerInfo_pkey";
       public            postgres    false    250            �           2606    32773    EventType EventType_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public."EventType"
    ADD CONSTRAINT "EventType_pkey" PRIMARY KEY ("EventTypeId");
 F   ALTER TABLE ONLY public."EventType" DROP CONSTRAINT "EventType_pkey";
       public            postgres    false    238            �           2606    24599     HeardResource HeardResource_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public."HeardResource"
    ADD CONSTRAINT "HeardResource_pkey" PRIMARY KEY ("HeardResourceId");
 N   ALTER TABLE ONLY public."HeardResource" DROP CONSTRAINT "HeardResource_pkey";
       public            postgres    false    230            �           2606    32805    PartyPackage PartyPackage_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."PartyPackage"
    ADD CONSTRAINT "PartyPackage_pkey" PRIMARY KEY ("PartyPackageId");
 L   ALTER TABLE ONLY public."PartyPackage" DROP CONSTRAINT "PartyPackage_pkey";
       public            postgres    false    248            �           2606    32843     PaymentStatus PaymentStatus_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public."PaymentStatus"
    ADD CONSTRAINT "PaymentStatus_pkey" PRIMARY KEY ("PaymentStatusId");
 N   ALTER TABLE ONLY public."PaymentStatus" DROP CONSTRAINT "PaymentStatus_pkey";
       public            postgres    false    256            �           2606    24605    Relationship Relationship_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."Relationship"
    ADD CONSTRAINT "Relationship_pkey" PRIMARY KEY ("RelationshipId");
 L   ALTER TABLE ONLY public."Relationship" DROP CONSTRAINT "Relationship_pkey";
       public            postgres    false    232            �           2606    24611    State State_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public."State"
    ADD CONSTRAINT "State_pkey" PRIMARY KEY ("StateId");
 >   ALTER TABLE ONLY public."State" DROP CONSTRAINT "State_pkey";
       public            postgres    false    234            �           2606    16417    Teams Teams_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Teams"
    ADD CONSTRAINT "Teams_pkey" PRIMARY KEY ("TeamId");
 >   ALTER TABLE ONLY public."Teams" DROP CONSTRAINT "Teams_pkey";
       public            postgres    false    220            �           2606    16409    TimeSlot TimeSlot_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."TimeSlot"
    ADD CONSTRAINT "TimeSlot_pkey" PRIMARY KEY ("TimeSlotId");
 D   ALTER TABLE ONLY public."TimeSlot" DROP CONSTRAINT "TimeSlot_pkey";
       public            postgres    false    218            �           2606    24622    Venue Venue_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Venue"
    ADD CONSTRAINT "Venue_pkey" PRIMARY KEY ("VenueId");
 >   ALTER TABLE ONLY public."Venue" DROP CONSTRAINT "Venue_pkey";
       public            postgres    false    235            �           2606    16403    user user_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY ("UserId");
 :   ALTER TABLE ONLY public."user" DROP CONSTRAINT user_pkey;
       public            postgres    false    216            
           2606    32861 *   Contract_PackageInfo_Character PackageInfo    FK CONSTRAINT     �   ALTER TABLE ONLY public."Contract_PackageInfo_Character"
    ADD CONSTRAINT "PackageInfo" FOREIGN KEY ("PackageInfoId") REFERENCES public."Contract_PackageInfo"("PackageInfoId");
 X   ALTER TABLE ONLY public."Contract_PackageInfo_Character" DROP CONSTRAINT "PackageInfo";
       public          postgres    false    260    258    4861                       2606    32872 ,   Contract_PackageInfo_Addon PackageInfo_Addon    FK CONSTRAINT     �   ALTER TABLE ONLY public."Contract_PackageInfo_Addon"
    ADD CONSTRAINT "PackageInfo_Addon" FOREIGN KEY ("PackageInfoId") REFERENCES public."Contract_PackageInfo"("PackageInfoId");
 Z   ALTER TABLE ONLY public."Contract_PackageInfo_Addon" DROP CONSTRAINT "PackageInfo_Addon";
       public          postgres    false    4861    262    258                       2606    32883 .   Contract_PackageInfo_Bounce PackageInfo_Bounce    FK CONSTRAINT     �   ALTER TABLE ONLY public."Contract_PackageInfo_Bounce"
    ADD CONSTRAINT "PackageInfo_Bounce" FOREIGN KEY ("PackageInfoId") REFERENCES public."Contract_PackageInfo"("PackageInfoId");
 \   ALTER TABLE ONLY public."Contract_PackageInfo_Bounce" DROP CONSTRAINT "PackageInfo_Bounce";
       public          postgres    false    258    4861    264                       2606    16429 #   Contract_TimeTeamInfo TeamsContrant    FK CONSTRAINT     �   ALTER TABLE ONLY public."Contract_TimeTeamInfo"
    ADD CONSTRAINT "TeamsContrant" FOREIGN KEY ("TeamId") REFERENCES public."Teams"("TeamId");
 Q   ALTER TABLE ONLY public."Contract_TimeTeamInfo" DROP CONSTRAINT "TeamsContrant";
       public          postgres    false    220    4823    222            	           2606    16434 &   Contract_TimeTeamInfo TimeSlotContrant    FK CONSTRAINT     �   ALTER TABLE ONLY public."Contract_TimeTeamInfo"
    ADD CONSTRAINT "TimeSlotContrant" FOREIGN KEY ("TimeSlotId") REFERENCES public."TimeSlot"("TimeSlotId");
 T   ALTER TABLE ONLY public."Contract_TimeTeamInfo" DROP CONSTRAINT "TimeSlotContrant";
       public          postgres    false    222    218    4821            �   &   x�3�tLI��S0�470�4�4�2��qZ@Eb���� ���      �       x�3����M�4�4�2��OK�L�pb���� a��      �   '   x�3�t�/�KNU0�410�4�4�2�	q�B�b���� ��      �   !   x�3��,N�4�4�2��M,.I-sb���� d#      �   "   x�3�tN,IM�/�T0�B.#��X F��� ��	9      �   *   x�3�t�H,JL.I-R0�4400�R\FH�F�F0�=... LHl      �      x�3�4A.#N#0m�i�c���� 3�j      �      x�3�44�B�=... F�      �   Z   x�3�t���NM�Qp,((�/�����2�rSS8����0q��%'��qCe8���˄3�4���4�tN�KN�� W� ��#�      �   �   x�u�]
!��3���Ώ�{��������QJ���N�m������:}Y������#]9�����ٸ����)�����X/�����/��2�U[�B���&n��m4��Y�f�v��ɘό��¶��ED>Ji1M      �   �  x���k��0�������!���9v����HEm�cl���)�t�8��o��BH�+N�A�+����.�q�#!�p�3�r˘� �W���5į>�T�ӑs������h�d�Ʒ���j��÷sc^�8ٛ�PWM$��ᅞ�n�ٍK3��K꟟��G$w�|��xH	�W�(=�"7 8A �)�D�3 %� ��e�Q��E�$� Eb��'@g DZq�.@~)�F�8 r�q��@�7-Ѵ �H(�(j^ ��p.�� =jp��8�uNH~���	'jq�h�1�<�A�2(�/��3�|0P�/�ꖉi'�A:wSu��:��LF���nX�A_��Va�Y���c�u�`y�P� `l���϶m7 ,|      �   �  x����n� �����0`ﲛv�6��}��&dU��Y�LS����z;��s2[��	?Q�M4{��ǔ�-���v��fdX)T\}�	Ճ��bQOP���������Y�3�-��P��'�4v�۬o_����T'��A���JO���Ig�J'U�U���;{U:��Uk�Uk�Uk�Uk5��jh��������к Wd�p ʓ,V\��v=_n/�B�q�{т�W4w���qp�;��8p��8($*��D�����m{
�1pq�W 1%��A�Я���XHd#Z�ӚA�<��� �#"�t���gB�D���!!U�H3H��������8�����B�}�L�aD_b��Kq$��.!�$����L��#�b{��!��6�ONCoWH=C;�ş�=M8a����~��[����ٗ�<�_�W      �   �   x�]�Kr1C��0S`����#h3	��L�z�fv\"ĤT?��ϲ`[�v%��6=�E�����?��z@_Ȣ���֣5�_�1�W`�]Ie�M�A=Ƴ4�9_nI��<`���*y6����@�����
l�������(�w��z'|��|e`���ܙ�y�Ҏ+��m�s�v�f���p�`���pgV`���sg����U���F^��+���|�Z�sS�      �   �   x�]�K�C1��0�|����1�&3���JE��q�W��J��� ��ٕHfl�z+E�����?�W=�o%�Eww)�[k6_�6�W`�]Ie�M�A7=ڳ4��_nI��6�H�V���=��� ���<�V`3�gLc��z&�i=��RI�v����ޏ����*ͳl�ݶ8���̳�ޣy}+�3+0���yg����U�����~יب����~tQ�w      �   �   x�e��1�\1`�v/鿎��$�|��h9К��S՗���cV�����a@6����
H�����\�k��fO6���z���7�I|2��8���Nl�ŉ�4;Rr� �ͻ%8�0J��ɑ�Hf���f��8��S g��G�J�,�=*dsI,:�U ��+�F� 9sK���d�J��m$s�F�q��\R�q; ��F�v�y�(_��Q����<�M;�      �   �  x����m�0�oM/� ���D*H�u<.FD ��G�����E��^Zi�P%y���ZڒTԋk���W�?}���E��%ВZ-��JYz�j��j�i�]-�v�e�N�Zm�ݣ���x&�1M���:�m|*m5�k�>Ժ�1�U���t�)9�֖��ݐ��)�.�(+�"��;��Rb��Eɣa#v�=���t:7�`d���Va}���wKE�ZO�J�h�w���^>S�5=䟽<KSiKVJ��c��g�;Wy��rU��ʵ�E;�lg]>�V���y�����Y4J'�AɃD�G�ֲ��X�|f��Nk�����v0f�V���������@tEOQ����GD]�di�{�<g*��Ӳ�vWO ��2F)*|���\�ht�4B��1d%��u>F�_�W��HE,�#��u���Y�T��F�����4PmV-�ڴڠ�H�7g�t�3,[ ���ֲ��vX��T�1��
<=P�xfoÂa�6�O���yO��j}�RQ/��C9^;c���K�;v��՞��7I�z����d)�� ���Yy��z��c����~��xv��ώ��ҙ����#^��\�a��Ğ����{f�w!S��f��f��5��y��7/�-w�R�<d�v�0�Un��8g��yX���hv�6ά�s,�8g��sD �����?�+~&      �   k  x��TK�� \?O�	R<@>�9����jb��X���cec�hI��^w�����.> ��a���  `���ȳ���i�w�Տv��i���IPK��*� |�����f�M��Ƕ�fV}���M�^s�y�n���N�H�(mS'�3A�X����~/�JJ�_H��au4ý_T�i�,r%c���w��?;��u�Rez��M#�s6[���%�N�K	T�m��C"8��Wʠi��_�kʁ?:xd�����#�	�(9�����T@F� �Y��������i��,C'����$��a��ġz�`������g��I�r �r:n�����gg��_�k�j�s3.UU��'4�      �   $   x�3�t�,*�HI��4�4�2��/�H-�c���� �@S      �   "   x�3�t��O�I�4�4�2��/�H-�c���� s�      �   '   x�3�H,*�THL�NLOU0�B.#4Q#�h� Xu      �   /   x�3�H�K��K�4�4�2�(�ON-.�9�3sRS��=... 
�      �       x�3�t*�/�H-�4�4�2�tK�sb���� ��J      �   $   x�3���K�L�4�4�2�H��,.I�sc���� �%:      �   m   x�-λ�PјWX���!	�C���m���0\��7N�Z�z�l�U��T��Z�Y[�n�U�uT��YŔ|�3�z�A�""B!���EP�"���GuZkKD:]      �   �   x�M�˩�@D�u+��鞿������P婍�P#4\�E�w�{�,,���?V�%����ݑ<�4����Yo�&kx���]���}{�o:�--�G���-=�Ҳ������g/�ẗ���B˕�����OO����7����ԧ�����tGz�?=ћ^�M��?ps�����tE��?�џ�OO���ß�u]_o/}}      �      x�3�K�+-6�B.#�̉���� w�      �   /   x�3�LK�,ŃRz��%�y�鹉�9z���0�NCNC�=... �5w     