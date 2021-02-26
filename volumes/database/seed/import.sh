#! /bin/bash
sleep 20
INDEX=$(mongo database:27017 --eval "db.getMongo().getDBNames().indexOf('CrazyPriceDb')" --quiet)
if [[ "$INDEX" -lt 0 ]]
then
 mongoimport --host database --db CrazyPriceDb --collection AllowedExternalUsers --drop --jsonArray --batchSize 1 --file /seed/AllowedExternalUsers.json
 mongoimport --host database --db CrazyPriceDb --collection Discounts --drop --jsonArray --batchSize 1 --file /seed/Discounts.json
 mongoimport --host database --db CrazyPriceDb --collection Users --drop --jsonArray --batchSize 1 --file /seed/Users.json
 mongoimport --host database --db CrazyPriceDb --collection Tags --drop --jsonArray --batchSize 1 --file /seed/Tags.json
 echo "***** CrazyPriceDb seeded. *****"
else
 echo "***** CrazyPriceDb already exists. *****"
fi