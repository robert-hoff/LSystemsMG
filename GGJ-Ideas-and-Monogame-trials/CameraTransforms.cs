using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GGJ_Ideas_and_Monogame_trials
{
    /*
     * It's not perfect, it is the world that rotates rather than calculating
     * a true orbit
     *
     */
    class CameraTransforms
    {
        // public Vector3 cameraPosition = new Vector3(3f, -3f, 7f);
        public Vector3 cameraPosition = new Vector3(3f, -7f, 3f);
        // public float cameraRotation = MathHelper.ToRadians(85);
        public float cameraRotation = MathHelper.ToRadians(0);
        private int viewportWidth;
        private int viewportHeight;

        private Matrix worldMatrix;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        public CameraTransforms(int viewportWidth, int viewportHeight) {
            this.viewportWidth = viewportWidth;
            this.viewportHeight = viewportHeight;
            CalculateWorldMatrix();
            CalculateViewMatrix();
            CalculateProjectionMatrix();
        }

        // -- World matrix and related updates
        //
        private void CalculateWorldMatrix()
        {
            worldMatrix = Matrix.CreateRotationZ(cameraRotation);
        }
        public void SetCameraOrbitDegrees(float cameraRotationDegrees)
        {
            this.cameraRotation = MathHelper.ToRadians(cameraRotationDegrees);
            CalculateWorldMatrix();
        }
        public void IncrementCameraOrbitDegrees(float cameraRotationDegrees)
        {
            this.cameraRotation += MathHelper.ToRadians(cameraRotationDegrees);
            CalculateWorldMatrix();
        }

        //
        // -- View matrix and related updates
        private void CalculateViewMatrix()
        {
            // viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, cameraZ), new Vector3(0, 0, 0), Vector3.UnitY);
            // viewMatrix = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0), Vector3.UnitY);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0), Vector3.UnitZ);
        }
        public void ZoomIn()
        {
            cameraPosition = Vector3.Multiply(cameraPosition, 0.9f);
            CalculateViewMatrix();
        }
        public void ZoomOut()
        {
            cameraPosition = Vector3.Multiply(cameraPosition, 1.1f);
            CalculateViewMatrix();
        }
        // instead of "orbit up" just raise the height
        public void OrbitUp()
        {
            float cameraHeight = cameraPosition.Z + 0.2f;
            cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y, cameraHeight);
            CalculateViewMatrix();
        }

        public void OrbitDown()
        {
            float cameraHeight = cameraPosition.Z - 0.2f;
            cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y, cameraHeight);
            CalculateViewMatrix();
        }


        //
        // -- Projection matrix and related updates
        private const float FOV = MathF.PI / 4; // 45 degrees
        // private const float FOV = 1.16937f; // 67 degrees
        private const float NEAR_CLIP = 0.1f;
        private const float FAR_CLIP = 100f;

        private void CalculateProjectionMatrix()
        {
            float viewPortAspectRatio = (float) viewportWidth / viewportHeight;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(FOV, viewPortAspectRatio, NEAR_CLIP, FAR_CLIP);
        }
        public void UpdateViewportDimensions(int viewportWidth, int viewportHeight)
        {
            if (this.viewportWidth != viewportWidth || this.viewportHeight != viewportHeight)
            {
                this.viewportWidth = viewportWidth;
                this.viewportHeight = viewportHeight;
                CalculateProjectionMatrix();
            }
        }

        // -- get the world,view and projection camera transforms
        public Matrix GetWorldMatrix()
        {
            return worldMatrix;
        }
        public Matrix GetViewMatrix()
        {
            return viewMatrix;
        }
        public Matrix GetProjectionMatrix()
        {
            return projectionMatrix;
        }
    }
}
