#! /bin/bash

mongoimport --host mongo --db CrazyPriceDb --collection Discounts --drop --jsonArray --batchSize 1 --file ./mongo-seed/Discounts.json

mongoimport --host mongo --db CrazyPriceDb --collection Users --drop --jsonArray --batchSize 1 --file ./mongo-seed/Users.json

mongoimport --host mongo --db CrazyPriceDb --collection Tags --drop --jsonArray --batchSize 1 --file ./mongo-seed/Tags.json