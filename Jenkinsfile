pipeline {
  agent any
  stages {
    stage('Verify Branch') {
      steps {
        echo "$GIT_BRANCH"
      }
    }
    stage('Run Unit Tests') {
      steps {
        sh(script: """
          cd Server
          dotnet test
          cd ..
        """)
      }
    }
    stage('Docker Build') {
      steps {
        sh(script: 'docker-compose build')
        sh(script: 'docker images -a')
      }
    }
    stage('Docker Build For Production') {
      when { branch 'master' }
      steps {
        sh(script: 'docker-compose -f docker-compose.yml -f production.yml build')
        sh(script: 'docker images -a')
      }
    }
    stage('Run Test Application') {
      steps {
        sh(script: 'docker-compose up -d')
      }
    }
    stage('Run Integration Tests') {
      steps {
        script {
          sleep 30
          try {
            sh "bash ./Tests/Startup.sh"
            sh "bash ./Tests/Register.sh"
            sh "bash ./Tests/Login.sh"
            sh "bash ./Tests/TopProducts.sh"
            sh "bash ./Tests/Statistics.sh"
          } catch (Exception e) {
            currentBuild.result = 'UNSTABLE'
          }
        }
      }
    }
    stage('Stop Test Application') {
      steps {
        sh(script: 'docker-compose down')
        // sh(script: 'docker volumes prune -f')
      }
      post {
        success {
          echo "Build successfull! You should deploy! :)"
        }
        failure {
          echo "Build failed! You should receive an e-mail! :("
        }
      }
    }
    stage('Push Images') {
      when { branch 'development' }
      steps {
        script {
          def images = [
            'zlatkorusev/microservices-architecture-identity-service',
            'zlatkorusev/microservices-architecture-customers-service',
            'zlatkorusev/microservices-architecture-statistics-service',
            'zlatkorusev/microservices-architecture-orders-service',
            'zlatkorusev/microservices-architecture-admin-service',
            'zlatkorusev/microservices-architecture-customers-gateway-service',
            'zlatkorusev/microservices-architecture-notifications-service',
            'zlatkorusev/microservices-architecture-monitoring-service',
            'zlatkorusev/microservices-architecture-client-development']

          for (int i = 0; i < images.size(); ++i) {
            docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
              def image = docker.image(images[i])
              image.push("1.0.${env.BUILD_ID}")
              image.push('latest')
            }
          }
        }
      }
    }
    stage('Deploy Development') {
      when { branch 'development' }
      steps {
        withKubeConfig([credentialsId: 'DevelopmentServer', serverUrl: 'https://EDC1391CAE9537B6F3D2163A7BAA4767.yl4.eu-south-1.eks.amazonaws.com']) {
		       sh(script: 'kubectl apply -f ./.k8s/.environment/development.yml')
		       sh(script: 'kubectl apply -f ./.k8s/databases')
		       sh(script: 'kubectl apply -f ./.k8s/event-bus')
		       sh(script: 'kubectl apply -f ./.k8s/web-services')
           sh(script: 'kubectl apply -f ./.k8s/clients')
           sh(script: 'kubectl set image deployments/user-client user-client=zlatkorusev/microservices-architecture-client-development:latest')
        }
      }
    }
    stage('Send Success Email') {
      steps {
        script {
            emailext (
              subject: "SUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
              body: """<p>SUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]':</p>
                <p>Check console output at <a href='${env.BUILD_URL}'>${env.JOB_NAME} [${env.BUILD_NUMBER}]</a></p>""",
              recipientProviders: [
                [$class: 'DevelopersRecipientProvider']
              ],
              replyTo: '$DEFAULT_REPLYTO',
              to: '$DEFAULT_RECIPIENTS'
            )
        }
      }
    }
  }
}