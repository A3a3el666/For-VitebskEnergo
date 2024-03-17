--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-03-17 17:24:47

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 16400)
-- Name: departments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.departments (
    departmentid integer NOT NULL,
    name character varying(255) NOT NULL,
    parentdepartmentid integer
);


ALTER TABLE public.departments OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16399)
-- Name: departments_departmentid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.departments_departmentid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.departments_departmentid_seq OWNER TO postgres;

--
-- TOC entry 4799 (class 0 OID 0)
-- Dependencies: 215
-- Name: departments_departmentid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.departments_departmentid_seq OWNED BY public.departments.departmentid;


--
-- TOC entry 218 (class 1259 OID 16412)
-- Name: employees; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employees (
    employeeid integer NOT NULL,
    departmentid integer NOT NULL,
    name character varying(255) NOT NULL,
    "position" character varying(255),
    phone character varying(20),
    email character varying(255)
);


ALTER TABLE public.employees OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16411)
-- Name: employees_employeeid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.employees_employeeid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.employees_employeeid_seq OWNER TO postgres;

--
-- TOC entry 4800 (class 0 OID 0)
-- Dependencies: 217
-- Name: employees_employeeid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.employees_employeeid_seq OWNED BY public.employees.employeeid;


--
-- TOC entry 4639 (class 2604 OID 16403)
-- Name: departments departmentid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments ALTER COLUMN departmentid SET DEFAULT nextval('public.departments_departmentid_seq'::regclass);


--
-- TOC entry 4640 (class 2604 OID 16415)
-- Name: employees employeeid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees ALTER COLUMN employeeid SET DEFAULT nextval('public.employees_employeeid_seq'::regclass);


--
-- TOC entry 4791 (class 0 OID 16400)
-- Dependencies: 216
-- Data for Name: departments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.departments (departmentid, name, parentdepartmentid) FROM stdin;
13	Отдел продаж	\N
14	Отдел маркетинга	13
15	Финансовый отдел	13
16	Отдел кадров	15
\.


--
-- TOC entry 4793 (class 0 OID 16412)
-- Dependencies: 218
-- Data for Name: employees; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employees (employeeid, departmentid, name, "position", phone, email) FROM stdin;
17	13	Иван Иванов	Менеджер по продажам	+1111	ivan@example.com
18	13	Анна Петрова	Специалист по клиентскому обслуживанию	+2222	anna@example.com
19	14	Алексей Смирнов	Маркетолог	+3333	alex@example.com
21	15	Павел Федоров	Финансовый аналитик	+4444	pavel@example.com
20	14	Екатерина Козлова	Координатор маркетинга	+5555	ekaterina@example.com
22	15	Мария Сидорова	Бухгалтер	+6666	maria@example.com
23	16	Дмитрий Николаев	HR-менеджер	+7777	dmitry@example.com
24	16	Ольга Иванова	Рекрутер	+8888	olga@example.com
25	16	Тест Тестовый	Тестировщик	+9999	test@test
\.


--
-- TOC entry 4801 (class 0 OID 0)
-- Dependencies: 215
-- Name: departments_departmentid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.departments_departmentid_seq', 16, true);


--
-- TOC entry 4802 (class 0 OID 0)
-- Dependencies: 217
-- Name: employees_employeeid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.employees_employeeid_seq', 25, true);


--
-- TOC entry 4642 (class 2606 OID 16405)
-- Name: departments departments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments
    ADD CONSTRAINT departments_pkey PRIMARY KEY (departmentid);


--
-- TOC entry 4644 (class 2606 OID 16419)
-- Name: employees employees_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY (employeeid);


--
-- TOC entry 4645 (class 2606 OID 16406)
-- Name: departments departments_parentdepartmentid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments
    ADD CONSTRAINT departments_parentdepartmentid_fkey FOREIGN KEY (parentdepartmentid) REFERENCES public.departments(departmentid);


--
-- TOC entry 4646 (class 2606 OID 16420)
-- Name: employees employees_departmentid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_departmentid_fkey FOREIGN KEY (departmentid) REFERENCES public.departments(departmentid);


-- Completed on 2024-03-17 17:24:47

--
-- PostgreSQL database dump complete
--

