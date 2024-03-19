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
                    bat """
                        echo "Exporting token and enterprise API to enable github-release tool"
                        export GITHUB_TOKEN=${GITHUB_TOKEN}

                        echo "Creating a new release in GitHub"
                        github-release release --repo ${https://github.com/zeroject/DivisorCounter} --tag ${v.1.0.0} --name "${test}"
                    """
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
