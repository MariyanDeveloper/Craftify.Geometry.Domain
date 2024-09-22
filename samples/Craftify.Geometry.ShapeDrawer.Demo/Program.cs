using Craftify.Geometry.Domain;
using Craftify.Geometry.ShapeDrawer;
using SkiaSharp;

var path = Path.Combine(Directory.GetCurrentDirectory(), "sample.png");
Console.WriteLine($"Path used for saving : {path}");
var arc = Arc.ByCenterPointRadiusEndAngle(
    Point.ByCoordinates(80, 400),
    50,
    -10,
    270,
    Vector.ZAxis()
);

var arcVisualization = Visualization.CreateArc(arc);

double[] parametersOnArc = [0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0];
var pointsOnArcVisualizations = parametersOnArc
    .Select(parameter => arc.GetPointAtParameter(parameter))
    .Select(point =>
        Visualization.CreatePoint(
            point,
            CreatePointArguments.Create(4, Style.ForPoint(SKColors.Red, 4))
        )
    )
    .Compose();

var centerArcVisualization = Visualization.CreatePoint(
    arc.Center,
    CreatePointArguments.Create(4, Style.ForPoint(SKColors.Green))
);

var polygonVisualizations = Visualization.CreateSmoothLines(
    Line.ByStartPointAndEndPoint(Point.ByCoordinates(30, 60), Point.ByCoordinates(350, 60)),
    Line.ByStartPointAndEndPoint(Point.ByCoordinates(350, 60), Point.ByCoordinates(350, 200)),
    Line.ByStartPointAndEndPoint(Point.ByCoordinates(350, 200), Point.ByCoordinates(300, 200)),
    Line.ByStartPointAndEndPoint(Point.ByCoordinates(300, 200), Point.ByCoordinates(300, 250)),
    Line.ByStartPointAndEndPoint(Point.ByCoordinates(300, 250), Point.ByCoordinates(30, 250)),
    Line.ByStartPointAndEndPoint(Point.ByCoordinates(30, 250), Point.ByCoordinates(30, 60))
);

DrawingBoard.Draw(
    Visualization.CreateComposite(
        polygonVisualizations,
        arcVisualization,
        centerArcVisualization,
        pointsOnArcVisualizations,
        (canvas, context) =>
        {
            var point1 = Point.ByCoordinates(250, 300);
            var point2 = Point.ByCoordinates(250, 400);
            var skPoint1 = point1
                .AdaptGeometryToStartFromLeftBottomCornerOnCanvas(context.Height)
                .MapTo2DSkPoint();

            var skPoint2 = point2
                .AdaptGeometryToStartFromLeftBottomCornerOnCanvas(context.Height)
                .MapTo2DSkPoint();
            var pointStyle = new SKPaint() { StrokeWidth = 6, Color = SKColors.Blue };

            var circleStyle = new SKPaint()
            {
                Color = SKColors.Black,
                StrokeWidth = 4,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
            };
            canvas.DrawPoint(skPoint1, pointStyle);
            canvas.DrawCircle(skPoint2, 3, circleStyle);
        }
    ),
    new ImageArguments(Width: 500, Height: 600, ImagePath: path, Background: SKColors.White)
);
