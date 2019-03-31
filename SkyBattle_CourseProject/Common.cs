
using System.Collections.Generic;

namespace SkyBattle_CourseProject
{
    public enum Projection
    {
        Perspective = 1,
        Orthogonal = 2,
        Frustum = 3
    }

    public class Point
    {
        public double X;
        public double Y;
        public double Z;
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Pillar
    {
        public double R;
        public double H;
        public Point Center;
        public Pillar(double r, double h, Point center)
        {
            R = r;
            H = h;
            Center = center;
        }

        public static List<Pillar> GetPillars()
        {
            var pillars = new List<Pillar>
            {
                new Pillar(0.1, 10, new Point(0, 0, 0)),
                new Pillar(0.1, 10, new Point(5, 10, 0)),
                new Pillar(0.1, 10, new Point(5, 0, 0)),
                new Pillar(0.1, 10, new Point(0, 10, 0))
            };
            return pillars;
        }
    }

    public class Explosion
    {
        public float[] Position { get; set; }
        public float _power { get; set; }
        public int MAX_PARTICLES { get; set; }
        public int _particles_now { get; set; }

        public bool IsStart { get; set; }

        public Partilce[] PartilceArray { get; set; }
        public bool IsDisplayList { get; set; }
        public int DisplayListNom { get; set; }

        public Explosion(float x, float y, float z, float power, int particle_count)
        {
            MAX_PARTICLES = 1000;
            IsStart = false;
            IsDisplayList = false;
            DisplayListNom = 0;

            Position = new float[3];
            Position[0] = x;
            Position[1] = y;
            Position[2] = z;

            _particles_now = particle_count;
            _power = power;

            if (particle_count > MAX_PARTICLES)
                particle_count = MAX_PARTICLES;
            PartilceArray = new Partilce[particle_count];


        }

        public void SetNewPosition(float x, float y, float z)
        {
            Position[0] = x;
            Position[1] = y;
            Position[2] = z;
        }

        public void SetNewPower(float new_power) =>
            _power = new_power;
    }

    public class Partilce
    {
        public float[] position { get; set; }
        public float _size { get; set; }
        public float _lifeTime { get; set; }

        public float[] Grav { get; set; }
        public float[] power { get; set; }
        public float attenuation { get; set; }

        public float[] speed { get; set; }

        public float LastTime { get; set; }

        public Partilce(float x, float y, float z, float size, float lifeTime, float start_time)
        {
            LastTime = 0;
            _size = size;
            _lifeTime = lifeTime;
            position = new float[3];
            position[0] = x;
            position[1] = y;
            position[1] = z;
            speed = new float[3];
            speed[0] = 0;
            speed[1] = 0;
            speed[2] = 0;
            Grav = new float[3];
            Grav[0] = 0;
            Grav[1] = -9.8f;
            Grav[2] = 0;
            attenuation = 3.33f;
            power = new float[3];
            power[0] = 0;
            power[0] = 0;
            power[0] = 0;
            LastTime = start_time;
        }
        public void SetPower(float x, float y, float z)
        {
            power[0] = x;
            power[1] = y;
            power[2] = z;
        }

        public void InvertSpeed(int os, float attenuation)
        {
            speed[os] *= -1 * attenuation;
        }

        public float GetSize()
        {
            return _size;
        }
        public void SetAttenuation(float new_value)
        {
            attenuation = new_value;
        }

        public void UpdatePosition(float timeNow)
        {
            float dTime = timeNow - LastTime;
            _lifeTime -= dTime;
            LastTime = timeNow;

            for (int a = 0; a < 3; a++)
            {
                if (power[a] > 0)
                {
                    power[a] -= attenuation * dTime;

                    if (power[a] <= 0)
                        power[a] = 0;
                }
                position[a] += (speed[a] * dTime + (Grav[a] + power[a]) * dTime * dTime);
                speed[a] += (Grav[a] + power[a]) * dTime;
            }
        }

        public bool IsLife() => _lifeTime > 0;

        public float GetPositionX() => position[0];

        public float GetPositionY() => position[1];

        public float GetPositionZ() => position[2];
    }
}
