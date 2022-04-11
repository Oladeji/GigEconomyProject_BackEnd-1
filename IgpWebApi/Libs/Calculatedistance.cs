public static class CalculateDistance
{

    public static double  getdistance(NetTopologySuite.Geometries.Point PointA , NetTopologySuite.Geometries.Point PointB,int srid=4326,int appropriateCoordinateSystem=2855)
    {
       // you can compare with result from 
       //https://www.movable-type.co.uk/scripts/latlong.html

       // var x = _dbctx.Clients.Where(i=>i.email=="Akomspatrick7@yahoo.com").First().Location;
       // var y = _dbctx.Clients.Where(i=>i.email=="Akomspatrick8@yahoo.com").First().Location;
      
       //This is to flip the latitudex and longy 
       var locationA = new NetTopologySuite.Geometries.Point(  PointA.Coordinate.Y , PointA.Coordinate.X ) { SRID = srid };
       var locationB = new NetTopologySuite.Geometries.Point( PointB.Coordinate.Y , PointB.Coordinate.X ) { SRID = srid };
       var distanceinMeters = locationA.ProjectTo(2855).Distance(locationB.ProjectTo(appropriateCoordinateSystem));
     //var distanceInMetersewn=CalculateDistance(newx,newy);

     //var distanceInMeters45 =CalculateDistance(a,b);

  return distanceinMeters;



    }

        public static  double getdistanceraw(NetTopologySuite.Geometries.Point point1, NetTopologySuite.Geometries.Point point2)
    {
        // var d1 = point1.Y * (Math.PI / 180.0);
        // var num1 = point1.X * (Math.PI / 180.0); //X for longitude 
        // var d2 = point2.Y * (Math.PI / 180.0);
        // var num2 = point2.X * (Math.PI / 180.0) - num1;
       //FLIP X AND Y
        var d1 = point1.X * (Math.PI / 180.0);
        var num1 = point1.Y * (Math.PI / 180.0); //X for longitude 
        var d2 = point2.X * (Math.PI / 180.0);
        var num2 = point2.Y * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                 Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}

/*
source    //https://www.movable-type.co.uk/scripts/latlong.html
Calculate distance, bearing and more between Latitude/Longitude points
This page presents a variety of calculations for lati­tude/longi­tude points, with the formulas and code fragments for implementing them.

All these formulas are for calculations on the basis of a spherical earth (ignoring ellipsoidal effects) – which is accurate enough* for most purposes… [In fact, the earth is very slightly ellipsoidal; using a spherical model gives errors typically up to 0.3%1 – see notes for further details].

Great-circle distance between two points
Enter the co-ordinates into the text boxes to try out the calculations. A variety of formats are accepted, principally:

deg-min-sec suffixed with N/S/E/W (e.g. 40°44′55″N, 73 59 11W), or
signed decimal degrees without compass direction, where negative indicates west/south (e.g. 40.7486, -73.9864):
Point 1:	
51.50083854242921 
 , 
-2.5475160219019983
Point 2:	
51.45540706793144 
 , 
-2.5916800766301478
Distance:	5.905 km (to 4 SF*)
Initial bearing:	211° 12′ 35″
Final bearing:	211° 10′ 30″
Midpoint:	51° 28′ 41″ N, 002° 34′ 11″ W
And you can see it on a map

Distance
This uses the ‘haversine’ formula to calculate the great-circle distance between two points – that is, the shortest distance over the earth’s surface – giving an ‘as-the-crow-flies’ distance between the points (ignoring any hills they fly over, of course!).

Haversine
formula:	a = sin²(Δφ/2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ/2)
c = 2 ⋅ atan2( √a, √(1−a) )
d = R ⋅ c
where	φ is latitude, λ is longitude, R is earth’s radius (mean radius = 6,371km);
note that angles need to be in radians to pass to trig functions!
JavaScript:	
const R = 6371e3; // metres
const φ1 = lat1 * Math.PI/180; // φ, λ in radians
const φ2 = lat2 * Math.PI/180;
const Δφ = (lat2-lat1) * Math.PI/180;
const Δλ = (lon2-lon1) * Math.PI/180;

const a = Math.sin(Δφ/2) * Math.sin(Δφ/2) +
          Math.cos(φ1) * Math.cos(φ2) *
          Math.sin(Δλ/2) * Math.sin(Δλ/2);
const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));

const d = R * c; // in metres
Note in these scripts, I generally use lat/lon for lati­tude/longi­tude in degrees, and φ/λ for lati­tude/longi­tude in radians – having found that mixing degrees & radians is often the easiest route to head-scratching bugs...

Historical aside: The height of tech­nology for navigator’s calculations used to be log tables. As there is no (real) log of a negative number, the ‘versine’ enabled them to keep trig func­tions in positive numbers. Also, the sin²(θ/2) form of the haversine avoided addition (which en­tailed an anti-log lookup, the addi­tion, and a log lookup). Printed tables for the haver­sine/in­verse-haver­sine (and its log­arithm, to aid multip­lica­tions) saved navi­gators from squaring sines, com­puting square roots, etc – arduous and error-prone activ­ities.

The haversine formula1 ‘remains particularly well-conditioned for numerical computa­tion even at small distances’ – unlike calcula­tions based on the spherical law of cosines. The ‘(re)versed sine’ is 1−cosθ, and the ‘half-versed-sine’ is (1−cosθ)/2 or sin²(θ/2) as used above. Once widely used by navigators, it was described by Roger Sinnott in Sky & Telescope magazine in 1984 (“Virtues of the Haversine”): Sinnott explained that the angular separa­tion between Mizar and Alcor in Ursa Major – 0°11′49.69″ – could be accurately calculated in Basic on a TRS-80 using the haversine.

For the curious, c is the angular distance in radians, and a is the square of half the chord length between the points.

If atan2 is not available, c could be calculated from 2 ⋅ asin( min(1, √a) ) (including protec­tion against rounding errors).

Using Chrome on an aging Core i5 PC, a distance calcula­tion takes around 2 – 5 micro­seconds (hence around 200,000 – 500,000 per second). Little to no benefit is obtained by factoring out common terms; probably the JIT compiler optimises them out.


*/