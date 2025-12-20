Algumas coisas a se salientar:
- O esquema de dados utilizado foi o PostgreSQL. É necessário possuir uma base de dados que esteja nos conformes com o DBContext desenvolvido na plataforma.
- Existem possíveis melhorias a serem realizada na API:
 1 - Utilização de um esquema de autenticação e autorização para os endpoints.
 2 - Logs para observabilidade da API.

Sobre a utilização da base de dados, visto a configuração necessária para a utilização da API, esta deve estar configurada da seguinte forma:

A tabela Order se refere aos registros de pedidos da plataforma. A chave primária da tabela (Id) faz uso de uma sequência que garante que todo registro será exclusivo e auto-incremental.
<img width="912" height="562" alt="image" src="https://github.com/user-attachments/assets/ba958e83-138f-4b8f-9074-bbb9238e2031" />
<img width="499" height="553" alt="image" src="https://github.com/user-attachments/assets/069cf23e-5aa5-40b0-9342-9c04af27c549" />

A tabela Product se refere aos registros de produtos da plataforma. A chave primária também funciona de forma exclusiva e auto-incremental. Aqui, é importante salientar a utilização do campo OrderId como uma chave estrangeira para a coluna Id da tabela Order.
<img width="904" height="564" alt="image" src="https://github.com/user-attachments/assets/a596b41d-fdd8-4a75-90e4-1a3ef90c1b3f" />
<img width="906" height="560" alt="image" src="https://github.com/user-attachments/assets/3d4f6502-0ab9-488f-bea2-f10bc4263f75" />
<img width="506" height="561" alt="image" src="https://github.com/user-attachments/assets/2958ef4f-40c5-4ab3-a1f5-fcc3b47e0b45" />

Por fim, existe uma função na base de dados que é utilizada no endpoint de listagem de pedidos. O script da procedure existe conforme abaixo:

"CREATE OR REPLACE FUNCTION public.getorderspaged(parampagesize integer, parampageindex integer, paramstatus integer DEFAULT NULL::integer) RETURNS TABLE("Id" bigint, "Status" smallint, "Description" character varying, "IsGift" boolean, "CreationDate" timestamp without time zone, "UpdateDate" timestamp without time zone, "DeletionDate" timestamp without time zone) LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE ROWS 1000 AS $BODY$ BEGIN RETURN QUERY SELECT o."Id", o."Status", o."Description", o."IsGift", o."CreationDate", o."UpdateDate", o."DeletionDate" FROM "Order" o WHERE o."DeletionDate" IS NULL AND (paramStatus IS NULL OR o."Status" = paramStatus) ORDER BY o."Id" DESC OFFSET COALESCE(paramPageIndex * paramPageSize, 0) LIMIT COALESCE(paramPageSize, NULL); END $BODY$;"
