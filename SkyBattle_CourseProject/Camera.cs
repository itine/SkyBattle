using Tao.OpenGl;

namespace SkyBattle_CourseProject
{
    class Camera
    {
        public Point Position;

        public Point Incline;
        public void SetCamera(Projection projection)
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            if (projection == Projection.Perspective)
            {
                Glu.gluPerspective(45, 1, 0.1, 5000);
            }
            Gl.glTranslated(Position.X, Position.Y, Position.Z);
            Gl.glRotated(Incline.X, 1, 0, 0);
            Gl.glRotated(Incline.Y, 0, 1, 0);
            Gl.glRotated(Incline.Z, 0, 0, 1);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }
    }
}
