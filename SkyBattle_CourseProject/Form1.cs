using System;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace SkyBattle_CourseProject
{
    public partial class Form1 : Form
    {
        private Camera _camera;
        private Texture _skyTexture;
        private Texture _ufoTexture;
        private Point _ufoCoord = new Point(2, 3, 2);
        private bool isBoomed = false;
        private float _globalTime = 0;
        private float _previousTime = 0;
        private Explosion _boom = new Explosion(1, -20, 1, 300, 500);
        private int _ufoAngle = 0;
        private const int _angleStep = -45;
        private const int _transferStep = -10;
        private bool _randomAnimationUsed = false;

        public Form1()
        {
            InitializeComponent();
            DrawWindow.InitializeContexts();
            _camera = new Camera
            {
                Position = new Point(0, 0, -15),
                Incline = new Point(0, 0, 0)
            };
            MouseWheel += OnMouseWheel;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Передвижения камеры осуществляются через клавиши wasd \n" +
               "Поворот камеры с помощью клавиш q и e \n" +
               "Наклон с помощью x и c \n " +
               "Приближение и удаление камеры колёсиком мыши \n " +
               "Куб можно двигать с помощью клавиш ijkl \n " +
               "Можно его так же взровать, нажав на клавишу b \n " +
               "Клавиши n и m реализуют масштабирование банки \n " +
               "С помощью ползунка можно вращать куб \n " +
               "Неконтролируемая анимация применяет все преобразования, \n" +
               "относительно времени.");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнил ст.гр. ПРИм-117 \n" +
                "Алексеев С.О. \n " +
                "Вариант №11 - воздушный бой");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            _globalTime += (float)timer1.Interval / 1000;
            _camera.SetCamera(Projection.Perspective);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            if (_randomAnimationUsed && (_globalTime - _previousTime) > 1)
            {
                _previousTime = _globalTime;
                if (_ufoAngle + _angleStep < 0)
                    _ufoAngle += 360;
                _ufoAngle += _angleStep;
                _ufoCoord = new Point(_ufoCoord.X, _ufoCoord.Y + _transferStep, _ufoCoord.Z);
            }
            DrawSky();
            if (!isBoomed)
                DrawUFO();
            DrawBoom(_globalTime);
            DrawWindow.Invalidate();
        }

        private void TrackBarBox_ValueChanged(object sender, EventArgs e)
        {
            _ufoAngle = trackBarBox.Value;
        }

        private void CheckBoxAnimation_CheckedChanged(object sender, EventArgs e)
        {
            _randomAnimationUsed = checkBoxAnimation.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, DrawWindow.Width, DrawWindow.Height);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            _skyTexture = new Texture("Textures\\sky.jpg");
            _ufoTexture = new Texture("Textures\\metallic.jpeg");
            timer1.Start();
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                _camera.Position = new Point(_camera.Position.X, _camera.Position.Y, _camera.Position.Z + 1);
            if (e.Delta < 0)
                _camera.Position = new Point(_camera.Position.X, _camera.Position.Y, _camera.Position.Z - 1);
        }

        #region Draw

        public void DrawBoom(float global_time)
        {
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glRotated(90, 1, 0, 0);
            Gl.glPushMatrix();
            Calculate(GetBoom(), global_time);
            Gl.glPopMatrix();
            Gl.glFlush();
        }

        public void DrawUFO()
        {
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_TEXTURE_GEN_S);
            Gl.glEnable(Gl.GL_TEXTURE_GEN_T);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, _ufoTexture.MGlTextureObject);
            Gl.glPushMatrix();
            Gl.glTranslated(_ufoCoord.X, _ufoCoord.Y, _ufoCoord.Z);
            Gl.glRotated(_ufoAngle, 0, 0, 1);
            Glut.glutSolidTetrahedron();
            Gl.glDisable(Gl.GL_TEXTURE_GEN_S);
            Gl.glDisable(Gl.GL_TEXTURE_GEN_T);
            Gl.glPopMatrix();
            Gl.glFlush();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        public void DrawSky()
        {
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, _skyTexture.MGlTextureObject);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Point lowerLeftPoint = new Point(-20, -40, 0);
            Point topRightPoint = new Point(20, 40, 0);
            Point lowerRightPoint = new Point(20, -40, 0);
            Point topLeftPoint = new Point(-20, 40, 0);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f((float)lowerLeftPoint.X, (float)lowerLeftPoint.Y, (float)lowerLeftPoint.Z);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f((float)lowerRightPoint.X, (float)lowerRightPoint.Y, (float)lowerRightPoint.Z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f((float)topRightPoint.X, (float)topRightPoint.Y, (float)topRightPoint.Z);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f((float)topLeftPoint.X, (float)topLeftPoint.Y, (float)topLeftPoint.Z);

            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f((float)lowerRightPoint.X, (float)lowerRightPoint.Y, (float)lowerRightPoint.Z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f((float)topRightPoint.X, (float)topRightPoint.Y, (float)topRightPoint.Z);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f((float)topLeftPoint.X, (float)topLeftPoint.Y, (float)topLeftPoint.Z);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f((float)lowerLeftPoint.X, (float)lowerLeftPoint.Y, (float)lowerLeftPoint.Z);

            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f((float)topLeftPoint.X, (float)topLeftPoint.Y, (float)topLeftPoint.Z);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f((float)lowerLeftPoint.X, (float)lowerLeftPoint.Y, (float)lowerLeftPoint.Z);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f((float)lowerRightPoint.X, (float)lowerRightPoint.Y, (float)lowerRightPoint.Z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f((float)topRightPoint.X, (float)topRightPoint.Y, (float)topRightPoint.Z);

            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f((float)topRightPoint.X, (float)topRightPoint.Y, (float)topRightPoint.Z);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f((float)topLeftPoint.X, (float)topLeftPoint.Y, (float)topLeftPoint.Z);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f((float)lowerLeftPoint.X, (float)lowerLeftPoint.Y, (float)lowerLeftPoint.Z);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f((float)lowerRightPoint.X, (float)lowerRightPoint.Y, (float)lowerRightPoint.Z);

            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f((float)lowerRightPoint.X, (float)lowerRightPoint.Y, (float)lowerRightPoint.Z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f((float)topRightPoint.X, (float)topRightPoint.Y, (float)topRightPoint.Z);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f((float)topLeftPoint.X, (float)topLeftPoint.Y, (float)topLeftPoint.Z);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f((float)lowerLeftPoint.X, (float)lowerLeftPoint.Y, (float)lowerLeftPoint.Z);

            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f((float)lowerLeftPoint.X, (float)lowerLeftPoint.Y, (float)lowerLeftPoint.Z);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f((float)lowerRightPoint.X, (float)lowerRightPoint.Y, (float)lowerRightPoint.Z);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f((float)topRightPoint.X, (float)topRightPoint.Y, (float)topRightPoint.Z);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f((float)topLeftPoint.X, (float)topLeftPoint.Y, (float)topLeftPoint.Z);

            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        #endregion


        #region Boom
        public void ChangeExplosion(float global_time)
        {
            Boooom(GetBoom(), global_time);
        }

        private void Boooom(Explosion boom, float time_start)
        {
            Random rnd = new Random();
            if (!boom.IsDisplayList)
            {
                CreateDisplayList(boom);
            }
            for (int ax = 0; ax < boom._particles_now; ax++)
            {
                boom.PartilceArray[ax] = new Partilce(boom.Position[0], boom.Position[1], boom.Position[2], 5.0f, 10, time_start);
                int direction_x = rnd.Next(1, 3);
                int direction_y = rnd.Next(1, 3);
                int direction_z = rnd.Next(1, 3);
                if (direction_x == 2)
                    direction_x = -1;
                if (direction_y == 2)
                    direction_y = -1;
                if (direction_z == 2)
                    direction_z = -1;
                float _power_rnd = rnd.Next((int)boom._power / 20, (int)boom._power);
                boom.PartilceArray[ax].SetAttenuation(boom._power / 2.0f);
                boom.PartilceArray[ax].SetPower(_power_rnd * (rnd.Next(100, 1000) / 1000.0f) * direction_x, _power_rnd * (rnd.Next(100, 1000) / 1000.0f) * direction_y, _power_rnd * (rnd.Next(100, 1000) / 1000.0f) * direction_z);
            }
            boom.IsStart = true;
        }

        private void CreateDisplayList(Explosion boom)
        {
            boom.DisplayListNom = Gl.glGenLists(1);
            Gl.glNewList(boom.DisplayListNom, Gl.GL_COMPILE);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.02f, 0.02f, 0);
            Gl.glVertex3d(0.02f, 0, -0.02f);
            Gl.glEnd();
            Gl.glEndList();
            boom.IsDisplayList = true;
        }

        private void Calculate(Explosion boom, float time)
        {
            if (boom.IsStart)
            {
                for (int ax = 0; ax < boom._particles_now; ax++)
                {
                    if (boom.PartilceArray[ax].IsLife())
                    {
                        boom.PartilceArray[ax].UpdatePosition(time);
                        Gl.glPushMatrix();
                        float size = boom.PartilceArray[ax].GetSize();
                        Gl.glTranslated(boom.PartilceArray[ax].GetPositionX(), boom.PartilceArray[ax].GetPositionY(), boom.PartilceArray[ax].GetPositionZ());
                        Gl.glScalef(size, size, size);
                        Gl.glCallList(boom.DisplayListNom);
                        Gl.glPopMatrix();
                        if (boom.PartilceArray[ax].GetPositionY() < 0)
                        {
                            boom.PartilceArray[ax].InvertSpeed(1, 0.6f);
                            boom.PartilceArray[ax].position[1] = 0;
                        }
                    }

                }
            }
        }

        public Explosion GetBoom()
        {
            _boom.SetNewPosition((float)_ufoCoord.X, (float)_ufoCoord.Y, (float)_ufoCoord.Z);
            _boom.SetNewPower(50);
            return _boom;
        }

        #endregion

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                _camera.Position = new Point(_camera.Position.X, _camera.Position.Y - 1, _camera.Position.Z);
            if (e.KeyCode == Keys.S)
                _camera.Position = new Point(_camera.Position.X, _camera.Position.Y + 1, _camera.Position.Z);
            if (e.KeyCode == Keys.A)
                _camera.Position = new Point(_camera.Position.X + 1, _camera.Position.Y, _camera.Position.Z);
            if (e.KeyCode == Keys.D)
                _camera.Position = new Point(_camera.Position.X - 1, _camera.Position.Y, _camera.Position.Z);
            if (e.KeyCode == Keys.W)
                _camera.Position = new Point(_camera.Position.X, _camera.Position.Y - 1, _camera.Position.Z);
            if (e.KeyCode == Keys.X)
                _camera.Incline = new Point(_camera.Incline.X + 1, _camera.Incline.Y, _camera.Incline.Z);
            if (e.KeyCode == Keys.C)
                _camera.Incline = new Point(_camera.Incline.X - 1, _camera.Incline.Y, _camera.Incline.Z);
            if (e.KeyCode == Keys.Q)
                _camera.Incline = new Point(_camera.Incline.X, _camera.Incline.Y, _camera.Incline.Z + 1);
            if (e.KeyCode == Keys.E)
                _camera.Incline = new Point(_camera.Incline.X, _camera.Incline.Y, _camera.Incline.Z - 1);
            if (e.KeyCode == Keys.B)
            {
                isBoomed = true;
                ChangeExplosion(_globalTime);
            }
            if (e.KeyCode == Keys.P)
            {
                _ufoCoord = new Point(_ufoCoord.X, _ufoCoord.Y + _transferStep, _ufoCoord.Z);
            }
            if (e.KeyCode == Keys.L)
                _ufoCoord = new Point(_ufoCoord.X + 1, _ufoCoord.Y, _ufoCoord.Z);
            if (e.KeyCode == Keys.J)
                _ufoCoord = new Point(_ufoCoord.X - 1, _ufoCoord.Y, _ufoCoord.Z);
            if (e.KeyCode == Keys.I)
                _ufoCoord = new Point(_ufoCoord.X, _ufoCoord.Y + 1, _ufoCoord.Z);
            if (e.KeyCode == Keys.K)
                _ufoCoord = new Point(_ufoCoord.X, _ufoCoord.Y - 1, _ufoCoord.Z);
        }
        
    }
}
