pipeline {
    agent any

    environment {
        GITHUB_TOKEN = credentials('github-token')
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
                    bat "curl -X POST -H 'Authorization: token ${GITHUB_TOKEN}' -H 'Content-Type: application/json' https://api.github.com/repos/DivisorCounter/releases -d '{\"tag_name\": \"v1.0.0\", \"name\": \"Release v1.0.0\", \"body\": \"${RELEASE_BODY}\"}'"
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
