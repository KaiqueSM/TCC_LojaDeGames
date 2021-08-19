drop database if exists db_pythongames;
create database db_pythongames
default character set utf8
default collate utf8_general_ci;

use db_pythongames;

CREATE TABLE tbl_entrega (
	cd_carrinho int PRIMARY KEY,
	dt_entrega date not null,
	vl_frete decimal(10,2) not null,
	no_cep varchar(9) not null,
	nm_estado varchar(50) not null,
	nm_cidade varchar(50) not null,
	nm_bairro varchar(50) not null,
	nm_rua varchar(50) not null,
	no_end int not null,
	nm_complemento varchar(30)
)default charset utf8;


CREATE TABLE tbl_carrinho (
	cd_carrinho int PRIMARY KEY  auto_increment,
	cd_funcionario int,
	cd_cliente int not null,
	cd_forma int not null,
	dt_venda date not null,
	vl_total decimal(10,2) not null
)default charset utf8;


CREATE TABLE tbl_itensCarrinho (
	cd_carrinho int,
	cd_produto int,
	qt_item int  not null,
	vl_item decimal(10,2) not null,
	PRIMARY KEY(cd_carrinho,cd_produto),
	FOREIGN KEY(cd_carrinho) REFERENCES tbl_carrinho (cd_carrinho)
)default charset utf8;


CREATE TABLE tbl_itensAbs (
	cd_abastecimento int,
	cd_produto int,
	qt_prod int  not null,
	PRIMARY KEY(cd_abastecimento,cd_produto)
)default charset utf8;


CREATE TABLE tbl_teste (
	cd_teste int PRIMARY KEY  auto_increment,
	cd_produto int  not null,
	cd_cliente int not null,
	dt_teste date  not null,
	hr_teste varchar(5)  not null
)default charset utf8;


CREATE TABLE tbl_produto (
	cd_produto int PRIMARY KEY  auto_increment,
	nm_prod varchar(50) not null,
    link_img varchar(200) not null,
	nm_categoria varchar(20) not null,
	vl_prod decimal(10,2) not null,
	qt_estoque int not null,
	prod_desc varchar(400) not null,
    flag bit default 0
)default charset utf8;
select * from tbl_produto;

CREATE TABLE tbl_suporte (
	cd_suporte int PRIMARY KEY  auto_increment,
	cd_produto int  not null,
	cd_carrinho int  not null,
	dt_sup date  not null,
	sup_descricao varchar(500)  not null,
    sup_status bit default 0,
	FOREIGN KEY(cd_produto) REFERENCES tbl_produto (cd_produto)
)default charset utf8;


CREATE TABLE tbl_detalheGame (
	cd_produto int PRIMARY KEY,
	nm_genero varchar(20) not null,
	vl_indicacao varchar(8) not null,
	FOREIGN KEY(cd_produto) REFERENCES tbl_produto (cd_produto)
)default charset utf8;


CREATE TABLE tbl_abastecimento (
	cd_abastecimento int PRIMARY KEY  auto_increment,
	cd_fornecedor int  not null,
	dt_abastecimento date not null
)default charset utf8;


CREATE TABLE tbl_funcionario (
	cd_funcionario int PRIMARY KEY  auto_increment,
	nm_func varchar(50)  not null,
	cpf_func varchar(14)  not null unique,
	nm_usu varchar(20)  not null unique,
	senha_usu varchar(16)  not null,
    func_acesso bit default 0,
	flag bit default 0
)default charset utf8;
/*insert into tbl_funcionario(nm_func,cpf_func,nm_usu,senha_usu,func_acesso,flag)
values('Kaique Mendonça','256.594.490-00','kaiqueMend','12345678',1,0);
select * from tbl_funcionario;*/
select * from tbl_funcionario;


CREATE TABLE tbl_fornecedor (
	cd_fornecedor int PRIMARY KEY  auto_increment,
	nm_fornecedor varchar(50) not null,
	no_cnpj varchar(18) not null unique,
	flag bit default 0
)default charset utf8;


CREATE TABLE tbl_pedido (
	cd_pedido int PRIMARY KEY  auto_increment,
	cd_funcionario int not null,
	cd_fornecedor int not null,
	dt_pedido date not null,
    ped_status bit default 0,
	FOREIGN KEY(cd_funcionario) REFERENCES tbl_funcionario (cd_funcionario),
	FOREIGN KEY(cd_fornecedor) REFERENCES tbl_fornecedor (cd_fornecedor)
)default charset utf8;


CREATE TABLE tbl_itensPedido (
	cd_pedido int,
	cd_produto int,
	qt_prod int not null,
	PRIMARY KEY(cd_pedido,cd_produto),
	FOREIGN KEY(cd_pedido) REFERENCES tbl_pedido (cd_pedido),
	FOREIGN KEY(cd_produto) REFERENCES tbl_produto (cd_produto)
)default charset utf8;


CREATE TABLE tbl_cliente (
	cd_cliente int PRIMARY KEY  auto_increment,
	nm_cli varchar(50) not null,
	cpf_cli varchar(14) not null unique,
	nm_email varchar(50) not null unique,
	senha_cli varchar(16) not null,
	flag bit default 0
)default charset utf8;


CREATE TABLE tbl_forma (
	cd_forma int PRIMARY KEY  auto_increment,
	nm_forma varchar(50)  not null
)default charset utf8;
/*
insert into tbl_forma(nm_forma)
values ('Cartão de Crédito'),
('Cartão de Débito'),
('Dinheiro na Loja'),
('Cartão de Crédito na Loja'),
('Cartão de Débitop na Loja');
select * from tbl_forma where cd_forma >2;
*/

CREATE TABLE tbl_cartao (
	cd_carrinho int PRIMARY KEY,
	nm_cartao varchar(50) not null,
	no_cartao varchar(19) not null,
	no_cvv varchar(3) not null,
	dt_validade date not null,
	FOREIGN KEY(cd_carrinho) REFERENCES tbl_carrinho (cd_carrinho)
)default charset utf8;


ALTER TABLE tbl_entrega ADD FOREIGN KEY(cd_carrinho) REFERENCES tbl_carrinho (cd_carrinho);
ALTER TABLE tbl_carrinho ADD FOREIGN KEY(cd_funcionario) REFERENCES tbl_funcionario (cd_funcionario);
ALTER TABLE tbl_carrinho ADD FOREIGN KEY(cd_cliente) REFERENCES tbl_cliente (cd_cliente);
ALTER TABLE tbl_carrinho ADD FOREIGN KEY(cd_forma) REFERENCES tbl_forma (cd_forma);
ALTER TABLE tbl_itensCarrinho ADD FOREIGN KEY(cd_produto) REFERENCES tbl_produto (cd_produto);
ALTER TABLE tbl_itensAbs ADD FOREIGN KEY(cd_abastecimento) REFERENCES tbl_abastecimento (cd_abastecimento);
ALTER TABLE tbl_itensAbs ADD FOREIGN KEY(cd_produto) REFERENCES tbl_produto (cd_produto);
ALTER TABLE tbl_teste ADD FOREIGN KEY(cd_produto) REFERENCES tbl_produto (cd_produto);
ALTER TABLE tbl_teste ADD FOREIGN KEY(cd_cliente) REFERENCES tbl_cliente (cd_cliente);
ALTER TABLE tbl_suporte ADD FOREIGN KEY(cd_carrinho) REFERENCES tbl_carrinho (cd_carrinho);
ALTER TABLE tbl_abastecimento ADD FOREIGN KEY(cd_fornecedor) REFERENCES tbl_fornecedor (cd_fornecedor);





/** -- Criando Views --**/

/** -- view para jogo mostrar os detalhes --**/
drop view if exists vw_game;
create view vw_game
as select  
	p.cd_produto, 
    p.nm_prod, 
    p.link_img, 
    p.nm_categoria, 
    p.vl_prod, 
    p.qt_estoque, 
    p.prod_desc, 
    p.flag, 
    d.nm_genero, 
    d.vl_indicacao
from tbl_produto as p inner join tbl_detalheGame as d
on p.cd_produto = d.cd_produto;
select * from vw_game;


/** -- view para itens do carrinho --**/
drop view if exists vw_itemCarrinho;
create view vw_itemCarrinho
as select  
	p.cd_produto, 
    p.nm_prod, 
    p.link_img, 
    p.nm_categoria, 
    p.vl_prod, 
    p.qt_estoque, 
    p.prod_desc, 
    i.cd_carrinho,
    i.qt_item,
    i.vl_item
from tbl_produto as p inner join tbl_itensCarrinho as i
on p.cd_produto = i.cd_produto;


/** -- view para carrinho --**/
drop view if exists vw_carrinho;
create view vw_carrinho
as select  
	car.cd_carrinho,
    car.cd_funcionario,
    car.cd_cliente,
    car.cd_forma,
    car.dt_venda,
    car.vl_total,
    cli.cpf_cli,
    cli.nm_cli,
    frm.nm_forma
from tbl_carrinho as car inner join tbl_cliente as cli
on cli.cd_cliente = car.cd_cliente
inner join tbl_forma as frm
on frm.cd_forma = car.cd_forma;


/** -- view para Pedido de Compra --**/
drop view if exists vw_pedido;
create view vw_pedido
as select  
	p.cd_pedido,
	p.cd_funcionario,
	p.cd_fornecedor,
	p.dt_pedido,
    p.ped_status,
    func.cpf_func,
    forn.no_cnpj,
    forn.nm_fornecedor
from tbl_pedido as p inner join tbl_funcionario as func
on p.cd_funcionario = func.cd_funcionario
inner join tbl_fornecedor as forn
on p.cd_fornecedor = forn.cd_fornecedor;


/** -- view para Ites do Pedido de Compra --**/
drop view if exists vw_itemPedido;
create view vw_itemPedido
as select  
	ip.cd_produto, 
	ip.cd_pedido, 
    p.nm_prod, 
    p.link_img, 
    ip.qt_prod
from tbl_itensPedido as ip inner join tbl_produto as p
on p.cd_produto = ip.cd_produto;


/** -- view para Abastecimento --**/
drop view if exists vw_abastecimento;
create view vw_abastecimento
as select  
	abs.cd_abastecimento,
	abs.cd_fornecedor,
    abs.dt_abastecimento,
    forn.no_cnpj,
    forn.nm_fornecedor
from tbl_abastecimento as abs inner join tbl_fornecedor as forn
on abs.cd_fornecedor = forn.cd_fornecedor;


/** -- view para Ites do Abastecimento --**/
drop view if exists vw_itemAbs;
create view vw_itemAbs
as select  
	ia.cd_produto, 
	ia.cd_abastecimento, 
    p.nm_prod, 
    p.link_img, 
    ia.qt_prod
from tbl_itensAbs as ia inner join tbl_produto as p
on p.cd_produto = ia.cd_produto;


/** -- view para Teste de game --**/
drop view if exists vw_teste;
create view vw_teste
as select  
	tst.cd_teste, 
	tst.cd_produto, 
    tst.cd_cliente, 
    c.nm_cli, 
    c.cpf_cli, 
    tst.dt_teste, 
    tst.hr_teste,
	p.nm_prod, 
    p.link_img
from tbl_teste as tst inner join tbl_produto as p
on p.cd_produto = tst.cd_produto
inner join tbl_cliente as c
on c.cd_cliente = tst.cd_cliente;


/** -- view para Suporte --**/
drop view if exists vw_suporte;
create view vw_suporte
as select  
	sup.cd_suporte, 
	sup.cd_produto, 
	sup.cd_carrinho, 
    c.nm_cli, 
    c.cpf_cli, 
    c.nm_email, 
    sup.dt_sup, 
    sup.sup_descricao,
    sup.sup_status,
	p.nm_prod, 
    p.link_img
from tbl_suporte as sup inner join tbl_produto as p
on p.cd_produto = sup.cd_produto
inner join tbl_carrinho as car
on car.cd_carrinho = sup.cd_carrinho
inner join tbl_cliente as c
on c.cd_cliente = car.cd_cliente;

/** -- ------------ --**/



/** -- Criando Procedures --**/

/** -- Procedure inserir game --**/
delimiter $$
drop procedure if exists sp_prod_insgame;
create procedure sp_prod_insgame(
	in nm varchar(50), 	
    in link varchar(200),
    in cat varchar(20),
    in vl decimal(10,2),
    in qtEst int,
	in descricao varchar(400),
	in gen varchar(20),
	in ind varchar(8)
)
begin
	declare vCdIns int;
	insert into tbl_produto(nm_prod, link_img, nm_categoria, vl_prod, qt_estoque, prod_desc)
    values (nm,link,cat,vl,qtEst,descricao);
	select max(cd_produto) into vCdIns from tbl_produto;
    
	insert into tbl_detalhegame(cd_produto, nm_genero, vl_indicacao)
    values (vCdIns,gen,ind);
end $$
delimiter ;


/** -- Procedure alterar game --**/
delimiter $$
drop procedure if exists sp_prod_altgame;
create procedure sp_prod_altgame(
	in cd int,
	in nm varchar(50), 	
    in link varchar(200),
    in cat varchar(20),
    in vl decimal(10,2),
    in qtEst int,
	in descricao varchar(400),
	in gen varchar(20),
	in ind varchar(8)
)
begin
	update tbl_produto
    set nm_prod = nm,
    link_img = link,
    nm_categoria = cat,
    vl_prod = vl,
    qt_estoque = qtEst,
    prod_desc = descricao
    where cd_produto = cd;

    delete from tbl_detalhegame where cd_produto = cd;
    
	insert into tbl_detalhegame(cd_produto, nm_genero, vl_indicacao)
    values (cd,gen,ind);
end $$
delimiter ;


/** -- Procedure alterar produto que não seja game --**/
delimiter $$
drop procedure if exists sp_prod_altprod;
create procedure sp_prod_altprod(
	in cd int,
	in nm varchar(50), 	
    in link varchar(200),
    in cat varchar(20),
    in vl decimal(10,2),
    in qtEst int,
	in descricao varchar(400)
)
begin
	update tbl_produto
    set nm_prod = nm,
    link_img = link,
    nm_categoria = cat,
    vl_prod = vl,
    qt_estoque = qtEst,
    prod_desc = descricao
    where cd_produto = cd;

    delete from tbl_detalhegame where cd_produto = cd;
end $$
delimiter ;

/** -- Procedure inserir abastecimento retornando o código inserido --**/
delimiter $$
drop procedure if exists sp_abs_insabs;
create procedure sp_abs_insabs(
	in cdForn int,
	in dtAbs date, 	
    out vCdIns int 
)
begin
	insert into tbl_abastecimento(cd_fornecedor, dt_abastecimento)
    values (cdForn,dtAbs);
    
    select max(cd_abastecimento) into vCdIns from tbl_abastecimento;	
end $$
delimiter ;

call sp_abs_insabs(1,'2021-05-26',@cd);
select @cd as cd;
select * from tbl_abastecimento;
/** -- ------------ --**/

/* Criação de Triggers */
/* Trigger para aumentar estoque ao abastecer */
delimiter $$
create trigger tg_insitensabs after insert
on tbl_itensabs for each row
begin
	update tbl_produto
    set qt_estoque = (qt_estoque + new.qt_prod)
    where cd_produto = new.cd_produto;
end $$
delimiter ;

/* Trigger para abaixar estoque ao inserir item de carrinho */
delimiter $$
create trigger tg_insitenscar after insert
on tbl_itenscarrinho for each row
begin
	update tbl_produto
    set qt_estoque = (qt_estoque - new.qt_item)
    where cd_produto = new.cd_produto;
end $$
delimiter ;
/** -- ------------ --**/
