#! /bin/sh

mongoimport --host mongo --db CrazyPriceDb --collection Discounts --drop --jsonArray --batchSize 1 --file /mongo_seed/Discounts.json
mongoimport --host mongo --db CrazyPriceDb --collection Users --drop --jsonArray --batchSize 1 --file /mongo_seed/Users.json
mongoimport --host mongo --db CrazyPriceDb --collection Tags --drop --jsonArray --batchSize 1 --file /mongo_seed/Tags.json