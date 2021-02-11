#! /bin/bash
sleep 20
if [ $(mongo mongo:27017 --eval 'db.getMongo().getDBNames().indexOf("CrazyPriceDb")' --quiet) -lt 0 ]; then
mongoimport --host mongo --db CrazyPriceDb --collection Discounts --drop --jsonArray --batchSize 1 --file /mongo_seed/Discounts.json
mongoimport --host mongo --db CrazyPriceDb --collection Users --drop --jsonArray --batchSize 1 --file /mongo_seed/Users.json
mongoimport --host mongo --db CrazyPriceDb --collection Tags --drop --jsonArray --batchSize 1 --file /mongo_seed/Tags.json
fi