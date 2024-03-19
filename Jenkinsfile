pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                // Build your project (e.g., compile code, run tests)
                sh 'dotnet build' // Example for Maven project, adjust for your build tool
            }
        }
    }
    
    post {
        success {
            echo 'Build and deployment successful!'
        }
        failure {
            echo 'Build or deployment failed!'
        }
    }
}
