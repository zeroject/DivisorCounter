pipeline {
    agent any

    environment {
        GITHUB_TOKEN = credentials('')
        TAG_NAME = 'v1.0.0'
        RELEASE_TITLE = 'Release v1.0.0'
        RELEASE_BODY = 'Release notes for v1.0.0'
    }
    
    stages {
        stage('Build') {
            steps {
                // Build your project (e.g., compile code, run tests)
                bat 'dotnet build' // Example for Maven project, adjust for your build tool
            }
        }
        stage('Release') {
            steps {
                script {
                    bat "gh release create ${TAG_NAME} -t '${RELEASE_TITLE}' -n '${RELEASE_BODY}'"
                }
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
