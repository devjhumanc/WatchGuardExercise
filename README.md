# WatchGuardExercise
Web API
Dot Net Core web api that calls https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&api_key=DEMO_KEY and return formatted json response.

1. .net Core 3.1 web api
2. linux docker support added to the project
3. pushed docker image to azure registry
4. published the container to azure web server

Sample call: https://watchguardmarsrover.azurewebsites.net/api/photos/2019-01-01
- when api is called, it calls nasa api, brings the images back, processes and saves in webserver and returns the formatted json response
- when same request is made again, it will grab the images from the saved location that way it reduces time in pulling from NASA and procesing it.

There is also a .net core console app in this repo that reads followings dates, from text file,
 -02/27/17
 -June 2, 2018
 -Jul-13-2016
 -April 31, 2018
 
 and downloads the images in the download folder of the user's machine
