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