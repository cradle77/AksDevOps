mongoimport --host composedemodb --port 27017 --db People --collection People --mode upsert --type json --file '/data/people.json' --jsonArray

while true
do 
  sleep 10000
done